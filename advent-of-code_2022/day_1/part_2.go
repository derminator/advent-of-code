package main

import (
	"advent-of-code_2022/day_1/lib"
	"fmt"
	"slices"
)

func main() {
	elfCals := lib.GetElfCals()
	slices.Sort(elfCals)
	length := len(elfCals)
	fmt.Println(elfCals[length-1] + elfCals[length-2] + elfCals[length-3])
}
