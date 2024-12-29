import java.io.File

val fileSections = File(".aoc/2024/5").readText().trim().split("\n\n")
val rules = fileSections[0].lines().map { it.split("|").let { (key, value) -> key to value } }
val updates = fileSections[1].lines().map { it.split(",") }

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
    val validUpdates = updates.filter { isUpdateValid(it, rules) }
    val middleValues = validUpdates.map { it[it.size / 2] }
    println(middleValues.sumOf { it.toInt() })
}

private val updateComparator = Comparator<String> { a, b ->
    0 // TODO
}

private fun part2() {
    val invalidUpdates = updates.filter { !isUpdateValid(it, rules) }
    val sortedLists = invalidUpdates.map { it.sortedWith(updateComparator) }
    val middleValues = sortedLists.map { it[it.size / 2] }
    println(middleValues.sumOf { it.toInt() })
}

fun main() {
    part1()
    part2()
}