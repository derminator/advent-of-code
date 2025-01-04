import java.io.File
import java.io.InvalidObjectException
import java.util.*

val fileSections = File(".aoc/2024/5").readText().trim().split("\n\n")
val rules = fileSections[0].lines().map { it.split("|").let { (key, value) -> key to value } }
val updates = fileSections[1].lines().map { it.split(",") }

private fun isUpdateValid(update: List<String>): Boolean {
    for ((i, value) in update.withIndex()) {
        for (rule in rules.filter { it.first == value }) {
            val otherValueIndex = update.indexOf(rule.second)
            if (otherValueIndex != -1 && i > otherValueIndex) return false
        }
    }
    return true
}

private fun part1() {
    val validUpdates = updates.filter { isUpdateValid(it) }
    val middleValues = validUpdates.map { it[it.size / 2] }
    println(middleValues.sumOf { it.toInt() })
}

private fun part2() {
    fun topologicalSort(update: List<String>): List<String> {
        val graph = mutableMapOf<String, MutableList<String>>()
        val inDegree = mutableMapOf<String, Int>()

        // Initialize the graph and in-degree map
        update.forEach { value ->
            graph[value] = mutableListOf()
            inDegree[value] = 0
        }

        // Build the graph and update in-degrees
        rules.forEach { (first, second) ->
            if (first in graph && second in graph) {
                graph[first]!!.add(second)
                inDegree[second] = inDegree[second]!! + 1
            }
        }

        // Topological sort using a stable priority queue
        val queue = PriorityQueue<Pair<Int, String>>(compareBy { it.first })
        var i = 0
        inDegree.filter { it.value == 0 }.forEach {
            queue.add(i to it.key)
            i++
        }
        val sortedList = mutableListOf<String>()

        while (queue.isNotEmpty()) {
            val (_, node) = queue.poll()
            sortedList.add(node)
            graph[node]!!.forEach { neighbor ->
                inDegree[neighbor] = inDegree[neighbor]!! - 1
                if (inDegree[neighbor] == 0) {
                    queue.add(update.indexOf(neighbor) to neighbor)
                }
            }
        }

        if (sortedList.size != update.size) {
            throw InvalidObjectException("Invalid update list: " + update.joinToString(", "))
        }
        return sortedList
    }

    val invalidUpdates = updates.filter { !isUpdateValid(it) }
    val sortedLists = invalidUpdates.map { topologicalSort(it) }
    val middleValues = sortedLists.map { it[it.size / 2] }
    println(middleValues.sumOf { it.toInt() })
}

fun main() {
    part1()
    part2()
}