module WordGuesser

open System

// Returns true if key is found in list
let rec checkCharacter list key =
    match list, key with
    | x :: xs, k when x = k -> true
    | x :: xs, k when x <> k -> checkCharacter xs k
    | [], _ -> false
    | _ -> failwith "checkCharacter failed!"

// Return a char array to display to the user
// characters in char array "listA" should be * unless it appears in char array "listB"
let rec displayWord listA listB =
    match listA, listB with
    | x :: xs, _ when checkCharacter listB x -> [ x ] @ displayWord xs listB // if found
    | x :: xs, _ when not (checkCharacter listB x) -> [ '*' ] @ displayWord xs listB
    | listA, _ -> listA
    | _ -> failwith "displayWord failed!"

// Play a round of the game, where a round is defined from when the word is only *
// until the word is completely guessed (no * characters in word)
let playRound (word: string) =
    let mutable playTurn = true
    let mutable charsWord = Seq.toList word
    let mutable guesses = []

    printfn " ------------------------------- "
    printfn "The length of the word is '%A'!" (List.length charsWord)

    while playTurn do
        printf "Guess: "
        let input = Console.ReadLine() |> string

        if input = "quit" then
            playTurn <- false
        else
            let inputList = Seq.toList input
            if (List.length inputList) = 1 then
                guesses <- List.append inputList guesses

                // Use (checkCharacter list key) to check if there is any * remaining
                // If there is: playTurn <- true
                // if there is not: playTurn <- false
                let isRemaining = checkCharacter (displayWord charsWord guesses) '*'

                let hiddenWord =
                    (displayWord charsWord guesses)
                    |> Array.ofList
                    |> String.Concat
                printfn "Word: %A  Used: %A Guessed: %A" hiddenWord guesses (List.item 0 inputList)
                printfn " ------------------------------- "
                if not isRemaining then
                    printfn "You won with %A guesses! Congratulations!" (List.length guesses)
                    printfn " ------------------------------- "
                    playTurn <- false
                else
                    playTurn <- true
            else
                printfn "input must be a single character or 'quit'!"
                playTurn <- true
    playTurn
