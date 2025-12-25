plugins {
    alias(libs.plugins.kotlinMultiplatform)
    alias(libs.plugins.kotlinxSerialization)
}

group = "com.github.derminator"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    commonMainImplementation(libs.kotlinxCoroutines)
}

kotlin {
    val nativeTargets = setOf(
        mingwX64()
    )

    nativeTargets.forEach { it.apply {
        binaries {
            val srcDir = layout.projectDirectory.dir("src/commonMain/kotlin").asFile
            srcDir.listFiles { f -> f.isDirectory && f.name.matches(Regex("day\\d+")) }
                .forEach { dayFile ->
                    val binaryName = dayFile.nameWithoutExtension // e.g., day01
                    executable(binaryName) {
                        entryPoint = "$binaryName.main"
                    }
                }
        }
    } }
}
