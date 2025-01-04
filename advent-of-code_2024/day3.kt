import java.io.File

private fun part1() {
    val input = File(".aoc/2024/3").readText()
    val regex = Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)")
    val matches = regex.findAll(input)
    var sum = 0
    matches.forEach {
        val left = it.groupValues[1].toInt()
        val right = it.groupValues[2].toInt()
        sum += left * right
    }
    println(sum)
}

private fun part2() {
    val input = File(".aoc/2024/3").readText()
    val regex = Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)|do\\(\\)|don't\\(\\)")
    val matches = regex.findAll(input)
    var sum = 0
    var enabled = true
    matches.forEach {
        if (it.value.startsWith("mul")) {
            if (enabled) {
                val left = it.groupValues[1].toInt()
                val right = it.groupValues[2].toInt()
                sum += left * right
            }
        } else if (it.value.startsWith("don")) {
            enabled = false
        } else {
            enabled = true
        }
    }
    println(sum)
}

fun main() {
    part1()
    part2()
}