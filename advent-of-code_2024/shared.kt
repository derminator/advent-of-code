import java.io.File

/**
 * Read a file into a list of line
 * Each line is a list of space-seperated entries
 */
fun splitFileLines(file: File): List<List<String>> {
    return file.readLines().map { s -> s.split("\\s+".toRegex()) }
}