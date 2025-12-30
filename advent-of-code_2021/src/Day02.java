import java.io.IOException;
import java.nio.file.Files;
import java.util.List;

public class Day02 {
    private static int horizontalPosition = 0;
    private static int part1Depth = 0;
    private static int part2Depth = 0;
    private static int part2Aim = 0;

    private static void move(String instruction) {
        var parts = instruction.split(" ");
        var amount = Integer.parseInt(parts[1]);
        switch (parts[0]) {
            case "forward":
                horizontalPosition += amount;
                part2Depth += part2Aim * amount;
                break;
            case "down":
                part1Depth += amount;
                part2Aim += amount;
                break;
            case "up":
                part1Depth -= amount;
                part2Aim -= amount;
                break;
        }
    }

    public static void main(String[] args) throws IOException {
        List<String> instructions = Files.readAllLines(Lib.getInputFile(2));
        for (String instruction : instructions) {
            move(instruction);
        }
        System.out.println("Part 1: " + (horizontalPosition * part1Depth));
        System.out.println("Part 2: " + (horizontalPosition * part2Depth));
    }
}
