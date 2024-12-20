package lib

func GetPriority(outlier uint8) uint {
	if outlier >= 'a' {
		return uint(outlier - 'a' + 1)
	} else {
		return uint(outlier - 'A' + 27)
	}
}
