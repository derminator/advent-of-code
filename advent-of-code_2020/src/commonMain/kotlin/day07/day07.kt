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

fun main() {
    var search = setOf("shiny gold")
    val searched = mutableSetOf<String>()
    while (search.isNotEmpty()) {
        val possibleContainers = search.flatMap { rules.filter { (_, contents) -> it in contents }.keys }.toSet()
        search = possibleContainers - searched
        searched.addAll(search)
    }
    println("Part 1: ${searched.size}")
}