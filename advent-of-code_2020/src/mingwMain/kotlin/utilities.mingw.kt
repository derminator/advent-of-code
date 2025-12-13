import kotlinx.cinterop.CPointer
import kotlinx.cinterop.ExperimentalForeignApi
import kotlinx.cinterop.addressOf
import kotlinx.cinterop.usePinned
import platform.posix.FILE
import platform.posix.SEEK_END
import platform.posix.SEEK_SET
import platform.posix.fclose
import platform.posix.fopen
import platform.posix.fread
import platform.posix.fseek
import platform.posix.ftell

@OptIn(ExperimentalForeignApi::class)
actual fun getFileContents(fileName: String): String {
    val file: CPointer<FILE> = fopen(fileName, "rb") ?: error("Cannot open file: $fileName")
    try {
        // Move to the end to get file size
        if (fseek(file, 0, SEEK_END) != 0) error("fseek end failed for $fileName")
        val size = ftell(file)
        if (size < 0) error("ftell failed for $fileName")
        // Rewind to start
        if (fseek(file, 0, SEEK_SET) != 0) error("fseek set failed for $fileName")

        require(size <= Int.MAX_VALUE) { "File too large: $fileName ($size bytes)" }

        val bytes = ByteArray(size)
        val readCount = bytes.usePinned { pinned ->
            // Read `intSize` items of 1 byte
            fread(pinned.addressOf(0), 1uL, size.toULong(), file)
        }
        if (readCount.toInt() != size) {
            error("Failed to read file: $fileName (read ${readCount.toLong()} of $size bytes)")
        }
        return bytes.decodeToString()
    } finally {
        fclose(file)
    }
}