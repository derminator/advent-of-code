import java.nio.file.Path;

public class Lib {
    public static Path getInputFile(int day) {
        return Path.of(".aoc/2021/" + day);
    }
}
