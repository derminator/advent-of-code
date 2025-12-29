import java.io.IOException;
import java.nio.file.Files;

public class Day01 {
    public static void main(String[] args) throws IOException {
        int[] depths = Files.readAllLines(Lib.getInputFile(1)).stream().mapToInt(Integer::parseInt).toArray();
        Integer prev = null;
        int part1Increases = 0;
        for (int depth : depths) {
            if (prev != null && depth > prev) {
                part1Increases++;
            }
            prev = depth;
        }
        System.out.println("Part 1: " + part1Increases);

        int part2Increases = 0;
        for (int i = 3; i < depths.length; i++) {
            int prevWindow = depths[i - 3] + depths[i - 2] + depths[i - 1];
            int currWindow = depths[i - 2] + depths[i - 1] + depths[i];
            if (currWindow > prevWindow) {
                part2Increases++;
            }
        }
        System.out.println("Part 2: " + part2Increases);
    }
}
