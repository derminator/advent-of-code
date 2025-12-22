package day07

import getInputLines

private val rules = getInputLines(7).associate {
    val (container, contents) = it.split(" bags contain ")
    val contentsMap = if (contents == "no other bags.") emptyMap() else contents.split(", ").associate { bag ->
        bag.removeSuffix(".").removeSuffix(" bags").removeSuffix(" bag").substringAfter(" ") to
                bag.substringBefore(" ").toInt()
    }
    container to contentsMap
}

private fun countBags(bag: String): Int {
    return rules[bag]?.entries?.fold(0) { acc, (bag, count) -> acc + count + count * countBags(bag) } ?: error("No bag $bag")
}

fun main() {
    var search = setOf("shiny gold")
    val searched = mutableSetOf<String>()
    while (search.isNotEmpty()) {
        val possibleContainers = search.flatMap { rules.filter { (_, contents) -> it in contents }.keys }.toSet()
        search = possibleContainers - searched
        searched.addAll(search)
    }
    println("Part 1: ${searched.size}")

    println("Part 2: ${countBags("shiny gold")}")
}