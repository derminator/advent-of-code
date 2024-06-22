package lib

func ReadRound(round string) (uint8, uint8) {
	return round[2], round[0]
}
