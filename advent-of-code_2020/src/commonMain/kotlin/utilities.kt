private fun getDayFileName(day: Int) = "../.aoc/2020/$day"

expect fun getFileContents(fileName: String): String

fun getInput(day: Int) = getFileContents(getDayFileName(day))

fun getInputLines(day: Int) = getInput(day).lines()