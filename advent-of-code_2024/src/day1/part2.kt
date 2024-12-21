package day1

fun main() {
    val (leftList, rightList) = readLists()
    var sum = 0

    for (item in leftList) {
        val occurrences = rightList.count { it == item }
        sum += occurrences * item
    }
    println(sum)
}