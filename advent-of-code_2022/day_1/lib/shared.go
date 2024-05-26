package lib

import (
	"bufio"
	"os"
	"strconv"
)

func catch(err error) {
	if err != nil {
		panic(err)
	}
}

func GetElfCals() []int {
	file, err := os.Open("day_1/input.txt")
	catch(err)
	defer func(reader *os.File) {
		err := reader.Close()
		catch(err)
	}(file)

	scanner := bufio.NewScanner(file)
	elfCals := make([]int, 0)
	currentElf := 0

	for scanner.Scan() {
		line := scanner.Text()
		if len(line) == 0 {
			elfCals = append(elfCals, currentElf)
			currentElf = 0
		} else {
			num, err := strconv.Atoi(line)
			catch(err)
			currentElf += num
		}
	}
	elfCals = append(elfCals, currentElf)
	return elfCals
}
