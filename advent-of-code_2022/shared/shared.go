package shared

import (
	"bufio"
	"os"
)

func Catch(err error) {
	if err != nil {
		panic(err)
	}
}

func ReadFileLines(path string, groupSeparators ...string) ([]string, []string) {
	pastSeperator := false
	file, err := os.Open(path)
	Catch(err)
	defer func(reader *os.File) {
		err := reader.Close()
		Catch(err)
	}(file)

	lines := make([]string, 0)
	lines2 := make([]string, 0)

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		text := scanner.Text()
		if pastSeperator {
			lines2 = append(lines2, text)
		} else if len(groupSeparators) > 0 && text == groupSeparators[0] {
			pastSeperator = true
		} else {
			lines = append(lines, text)
		}
	}

	return lines, lines2
}

func StringToChars(input string) []uint8 {
	ret := make([]uint8, len(input))
	for i := 0; i < len(input); i++ {
		ret[i] = input[i]
	}
	return ret
}
