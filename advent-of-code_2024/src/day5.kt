import java.io.File

val fileSections = File(".aoc/2024/5").readText().trim().split("\n\n")

private fun isUpdateValid(update: List<String>, rules: List<Pair<String, String>>): Boolean {
    for ((i, value) in update.withIndex()) {
        for (rule in rules.filter { it.first == value }) {
            val otherValueIndex = update.indexOf(rule.second)
            if (otherValueIndex != -1 && i > otherValueIndex) return false
        }
    }
    return true
}

private fun part1() {
    val (rulesText, updatesText) = fileSections
    val rules = rulesText.lines().map { it.split("|").let { (key, value) -> key to value } }
    val updates = updatesText.lines().map { it.split(",") }
    val validUpdates = updates.filter { isUpdateValid(it, rules) }
    val middleValues = validUpdates.map { it[it.size / 2] }
    println(middleValues.sumOf { it.toInt() })
}

fun main() {
    part1()
}