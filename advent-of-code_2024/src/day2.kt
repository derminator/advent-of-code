import java.io.File
import kotlin.math.abs

private fun isReportSafe(report: List<Int>, useDampener: Boolean): Boolean {
    var prev: Int? = null
    var asc: Boolean? = null
    var usedDampener = !useDampener

    for (level in report) {
        if (prev != null) {
            val diff = abs(level - prev)
            if (diff < 1 || diff > 3) {
                if (usedDampener) return false
                else {
                    usedDampener = true
                    continue
                }
            }
            else if (asc == null) asc = level > prev
            else if (asc != (level > prev)) {
                if (usedDampener) return false
                else {
                    usedDampener = true
                    continue
                }
            }
        }
        prev = level
    }
    return true
}

private fun readReports(): List<List<Int>> {
    return splitFileLines(File(".aoc/2024/2")).map { it.map { s -> s.toInt() } }
}

private fun part1() {
    val reports = readReports()
    println(reports.count { isReportSafe(it, false) })
}

private fun part2() {
    val reports = readReports()
    println(reports.count { isReportSafe(it, true) })
}

fun main() {
    part1()
    part2()
}