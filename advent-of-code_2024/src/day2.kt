import java.io.File
import kotlin.math.abs

private fun isReportSafe(report: List<Int>): Boolean {
    var prev: Int? = null
    var asc: Boolean? = null

    for (level in report) {
        if (prev != null) {
            val diff = abs(level - prev)
            if (diff < 1 || diff > 3) {
                return false
            }
            else if (asc == null) asc = level > prev
            else if (asc != (level > prev)) {
                return false
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
    println(reports.count { isReportSafe(it) })
}

private fun part2() {
    val reports = readReports()
    println(reports.count { report ->
        isReportSafe(report) || report.indices.any { i ->
            isReportSafe(report.filterIndexed { index, _ -> index != i })
        }
    })
}

fun main() {
    part1()
    part2()
}