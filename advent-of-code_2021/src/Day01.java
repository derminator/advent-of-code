import java.io.IOException;
import java.nio.file.Files;

public class Day01 {
    public static void main(String[] args) throws IOException {
        int[] depths = Files.readAllLines(Lib.getInputFile(1)).stream().mapToInt(Integer::parseInt).toArray();
        Integer prev = null;
        int increases = 0;
        for (int depth : depths) {
            if (prev != null && depth > prev) {
                increases++;
            }
            prev = depth;
        }
        System.out.println("Part 1: " + increases);
    }
}
