plugins {
    alias(libs.plugins.kotlinMultiplatform)
    alias(libs.plugins.kotlinxSerialization)
}

group = "com.github.derminator"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

kotlin {
    val hostOs = System.getProperty("os.name")
    val isArm64 = System.getProperty("os.arch") == "aarch64"
    val isMingwX64 = hostOs.startsWith("Windows")
    val sourceSetName = "host"
    val nativeTarget = when {
        hostOs == "Mac OS X" && isArm64 -> macosArm64(sourceSetName)
        hostOs == "Mac OS X" && !isArm64 -> macosX64(sourceSetName)
        hostOs == "Linux" && isArm64 -> linuxArm64(sourceSetName)
        hostOs == "Linux" && !isArm64 -> linuxX64(sourceSetName)
        isMingwX64 -> mingwX64(sourceSetName)
        else -> throw GradleException("Host OS is not supported in Kotlin/Native.")
    }

    nativeTarget.apply {
        binaries {
            executable {
                entryPoint = "main"
            }
        }
    }

    sourceSets {
        nativeMain.dependencies {
            implementation(libs.kotlinxSerializationJson)
        }
    }
}
