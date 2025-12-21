package day06

import getInputLines

private class Group(answers: List<String>) {
    private val yesAnswers = answers.flatMap { it.toList() }.toSet()
    val numberOfYesAnswers = yesAnswers.size
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
}