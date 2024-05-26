package main

import (
	"advent-of-code_2022/day_1/lib"
	"fmt"
	"slices"
)

func main() {
	elfCals := lib.GetElfCals()

	fmt.Println(slices.Max(elfCals))
}
