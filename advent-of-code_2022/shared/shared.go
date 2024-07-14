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

func ReadFileLines(path string) []string {
	file, err := os.Open(path)
	Catch(err)
	defer func(reader *os.File) {
		err := reader.Close()
		Catch(err)
	}(file)

	lines := make([]string, 0)

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	return lines
}

func StringToChars(input string) []uint8 {
	ret := make([]uint8, len(input))
	for i := 0; i < len(input); i++ {
		ret[i] = input[i]
	}
	return ret
}
