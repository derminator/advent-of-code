package day1

import java.io.File

internal fun readLists(): Pair<MutableList<Int>, MutableList<Int>> {
    val file = File("input.txt")

    val leftList = mutableListOf<Int>()
    val rightList = mutableListOf<Int>()

    file.forEachLine {
        val parts = it.split("\\s+".toRegex())
        leftList.add(parts[0].toInt())
        rightList.add(parts[1].toInt())
    }
    return Pair(leftList, rightList)
}