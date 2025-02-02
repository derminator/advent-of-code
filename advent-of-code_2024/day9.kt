import java.io.File

val diskMap = File(".aoc/2024/9").readText().toCharArray().map { it.toString().toInt() }

private fun part1() {
    val blockMap = generateBlockMap()

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

    val checksum = calculateChecksum(blockMap)
    println(checksum)
}

private fun findEmptyBlock(blockMap: MutableList<Int?>, length: Int): Int? {
    var potentialStart: Int? = null
    blockMap.forEachIndexed { index, block ->
        if (block == null) {
            if (potentialStart == null) {
                potentialStart = index
            }

            if (index - potentialStart!! >= length) {
                return potentialStart
            }
        } else if (potentialStart != null) {
            potentialStart = null
        }
    }
    return null
}

private fun part2() {
    val blockMap = generateBlockMap()

    // Move full files one at a time from the end to the first area with enough space
    var currentFile: Int? = null
    var currentFileEnd = 0
    for (i in blockMap.size - 1 downTo 0) {
        if (blockMap[i] != currentFile) {
            if (currentFile != null) {
                val length = currentFileEnd - i
                // Find the first empty block that can fit the file
                val target = findEmptyBlock(blockMap, length)
                if (target != null) {
                    for (j in 0 until length) {
                        blockMap[target + j] = currentFile
                        blockMap[currentFileEnd - j] = null
                    }
                }
            }

            currentFile = blockMap[i]
            currentFileEnd = i
        }
    }

    val checksum = calculateChecksum(blockMap)
    println(checksum)
}

private fun calculateChecksum(blockMap: MutableList<Int?>): Long {
    val checksum = blockMap.foldIndexed(0L) { index, acc, block ->
        if (block != null) {
            acc + index * block
        } else {
            acc
        }
    }
    return checksum
}

private fun generateBlockMap(): MutableList<Int?> {
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
    return blockMap
}

private fun main() {
    part1()
    part2()
}