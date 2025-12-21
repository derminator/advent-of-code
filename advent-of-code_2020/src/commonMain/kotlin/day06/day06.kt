package day06

import getInputLines

private class Group(answers: List<String>) {
    val numberOfYesAnswers = answers.flatMap { it.toList() }.toSet().size
    val numberOfCommonYesAnswers = answers.map { it.toSet() }.reduce { acc, set -> acc.intersect(set) }.size
}

fun main() {
    val groups = getInputLines(6).fold(mutableListOf(mutableListOf<String>())) { acc, line ->
        if (line.isBlank()) {
            acc.add(mutableListOf())
        } else {
            acc.last().add(line)
        }
        acc
    }.map { Group(it) }
    println("Part 1: ${groups.sumOf { it.numberOfYesAnswers }}")
    println("Part 2: ${groups.sumOf { it.numberOfCommonYesAnswers }}")
}