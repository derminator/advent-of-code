import java.io.File

val equations = File(".aoc/2024/7").readLines().map { line ->
    val (key, value) = line.split(":")
    key.trim().toLong() to value.trim().split("\\s+".toRegex()).map { it.toLong() }
}

private fun part1() {
    val validCalibrations = equations.filter { (result, values) ->
        var possiblePrevValues = listOf(values[0])
        for (i in 1 until values.size) {
            val newPrevValues = ArrayList<Long>()
            possiblePrevValues.forEach {
                newPrevValues.add(it + values[i])
                newPrevValues.add(it * values[i])
            }
            possiblePrevValues = newPrevValues
        }
        possiblePrevValues.contains(result)
    }
    println(validCalibrations.sumOf { it.first })
}

private fun part2() {
    val validCalibrations = equations.filter { (result, values) ->
        var possiblePrevValues = listOf(values[0])
        for (i in 1 until values.size) {
            val newPrevValues = ArrayList<Long>()
            possiblePrevValues.forEach {
                newPrevValues.add(it + values[i])
                newPrevValues.add(it * values[i])
                newPrevValues.add((it.toString() + values[i].toString()).toLong())
            }
            possiblePrevValues = newPrevValues
        }
        possiblePrevValues.contains(result)
    }
    println(validCalibrations.sumOf { it.first })
}

fun main() {
    part1()
    part2()
}