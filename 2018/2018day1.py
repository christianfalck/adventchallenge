def parse_file(file_path):
    start = 0
    with open(file_path, 'r') as file:
        for line in file:
            # Convert the line to an integer and add it to start
            start += int(line.strip())  # Use strip() to remove leading/trailing whitespaces

    print(start)

file_path = 'C:\\Users\\falke\\Documents\\Projects\\2018\\2018day1.txt'
parse_file(file_path)