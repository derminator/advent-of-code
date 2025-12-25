package day10

import getInputLines
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import kotlin.concurrent.AtomicLong

private val adaptors = getInputLines(10).map { it.toInt() }.sorted()
private val maxJoltage = adaptors.last() + 3
private var validPatterns = AtomicLong(1L)

private fun List<Int>.previous(index: Int): Int {
    return getOrElse(index - 1) {0}
}

private fun List<Int>.next(index: Int): Int {
    return getOrElse(index + 1) {maxJoltage}
}

private suspend fun List<Int>.removeAndCheck(index: Int): Unit = coroutineScope {
    val previous = previous(index)
    val next = next(index)
    val newList = toMutableList().apply {
        removeAt(index)
        if ((next - previous) !in 1..3) {
            return@coroutineScope
        }
    }
    validPatterns.incrementAndGet()
    for (i in index + 1 until newList.size) {
        launch { newList.removeAndCheck(i) }
    }
}

fun main() {
    val differences = adaptors.zipWithNext { a, b -> b - a } + adaptors[0] + 3
    println("Part 1: ${differences.count { it == 1 } * differences.count { it == 3}}")

    runBlocking(Dispatchers.Default) {
        adaptors.indices.forEach {
            launch { adaptors.removeAndCheck(it) }
        }
    }
    println("Part 2: $validPatterns")
}