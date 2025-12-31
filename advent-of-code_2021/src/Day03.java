import java.io.IOException;
import java.nio.file.Files;
import java.util.Arrays;

public class Day03 {
    public static void main(String[] args) throws IOException {
        Report report = new Report();
        System.out.println("Part 1: " + (report.gamma * report.epsilon));
    }

    private static class Report {
        private final long gamma;
        private final long epsilon;

        public Report() throws IOException {
            Boolean[][] bits = Files.readAllLines(Lib.getInputFile(3)).stream()
                    .map(line -> line.chars().mapToObj(c -> c == '1').toArray(Boolean[]::new))
                    .toArray(Boolean[][]::new);

            final var numberLength = bits[0].length;
            var bitStats = new BitStat[numberLength];
            for (int i = 0; i < numberLength; i++) {
                final int index = i;
                var trueCount = Arrays.stream(bits).filter(number -> number[index]).count();
                var falseCount = bits.length - trueCount;
                bitStats[i] = new BitStat(trueCount, falseCount);
            }

            var gammaBits = Arrays.stream(bitStats).map(stat -> stat.trueCount > stat.falseCount).toArray(Boolean[]::new);
            gamma = parseBinary(gammaBits);

            var epsilonBits = Arrays.stream(gammaBits).map(bit -> !bit).toArray(Boolean[]::new);
            epsilon = parseBinary(epsilonBits);
        }

        private long parseBinary(Boolean[] bits) {
            long result = 0;
            for (boolean bit : bits) {
                result <<= 1;
                result |= bit ? 1L : 0L;
            }
            return result;
        }

        private record BitStat(long trueCount, long falseCount) {
        }
    }
}
