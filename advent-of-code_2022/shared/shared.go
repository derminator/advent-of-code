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
