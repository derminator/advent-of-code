package day1

import kotlin.math.abs

fun main() {
    val (leftList, rightList) = readLists()

    leftList.sort()
    rightList.sort()

    var sum = 0

    for (i in leftList.indices) {
        sum += abs(leftList[i] - rightList[i])
    }

    println(sum)
}