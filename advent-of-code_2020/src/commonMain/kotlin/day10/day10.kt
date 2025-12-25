package day10

import getInputLines
import kotlinx.coroutines.Deferred
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.async
import kotlinx.coroutines.runBlocking

private val adaptors = getInputLines(10).map { it.toInt() }.sorted()
private val maxJoltage = adaptors.last() + 3

fun main() {
    val differences = adaptors.zipWithNext { a, b -> b - a } + adaptors[0] + 3
    println("Part 1: ${differences.count { it == 1 } * differences.count { it == 3}}")

    runBlocking(Dispatchers.Default) {
        val fullList = buildList {
            add(0)
            addAll(adaptors)
            add(maxJoltage)
        }
        val possibleRoutes: List<Deferred<Long>> = buildList {
            fullList.indices.forEach { i ->
                add(async {
                    if (i == 0) return@async 1
                    var totalPaths = 0L
                    for (j in 0 until i) {
                        if (fullList[i] - fullList[j] in 1..3) {
                            totalPaths += get(j).await()
                        }
                    }
                    totalPaths
                })
            }
        }
        println("Part 2: ${possibleRoutes.last().await()}")
    }
}