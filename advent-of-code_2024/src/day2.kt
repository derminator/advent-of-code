import java.io.File
import kotlin.math.abs

private fun isReportSafe(report: List<Int>): Boolean {
    var prev: Int? = null
    var asc: Boolean? = null

    for (level in report) {
        if (prev != null) {
            val diff = abs(level - prev)
            if (diff < 1 || diff > 3) return false
            else if (asc == null) asc = level > prev
            else if (asc != (level > prev)) return false
        }
        prev = level
    }
    return true
}

private fun part1() {
    val reports = splitFileLines(File(".aoc/2024/2")).map { it.map { s -> s.toInt() } }
    println(reports.count { isReportSafe(it) })
}

fun main() {
    part1()
}