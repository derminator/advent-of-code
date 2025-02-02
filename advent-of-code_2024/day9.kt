import java.io.File

private fun part1() {
    val diskMap = File(".aoc/2024/9").readText().toCharArray().map { it.toString().toInt() }
    val blockMap = mutableListOf<Int?>()
    var isFile = true
    var fileID = 0
    diskMap.forEach {
        if (isFile) {
            for (i in 0 until it) {
                blockMap.add(fileID)
            }
            fileID++
        } else {
            for (i in 0 until it) {
                blockMap.add(null)
            }
        }
        isFile = !isFile
    }

    // Move file blocks from the end one block at a time to the first empty block
    for (i in blockMap.size - 1 downTo 0) {
        if (blockMap[i] != null) {
            val target = blockMap.indexOf(null)
            if (target > i) {
                break
            }
            blockMap[target] = blockMap[i].also { blockMap[i] = null }
        }
    }

    // Calculate checksum
    val checksum = blockMap.foldIndexed(0L) { index, acc, block ->
        if (block != null) {
            acc + index * block
        } else {
            acc
        }
    }

    println(checksum)
}

private fun main() {
    part1()
}