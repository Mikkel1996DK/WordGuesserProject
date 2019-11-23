open System

 
[<EntryPoint>]
let main argv =
    printfn " ------------------------------- "
    printfn "| Welcome to Word Guesser! ^_^  |"
    printfn "| Type 'quit' to quit the game. |"

    let mutable hasQuit = false
    while not hasQuit do

        let rngGen = Random()
        let mutable word =
            let wordList = [ "toyota"; "suzuki"; "volkswagen"; "opel"; "dodge"; "chevrolet"; "fiat"; "skoda" ]
            let index = List.length wordList |> rngGen.Next
            List.item index wordList

        // Avoid annoying implicitly-ignored "warning" by putting into a let below...
        let b = WordGuesser.playRound word 

        printfn "Do you want to retry?"
        printf "(y/n): "
        let retry = Console.ReadLine()
        if (Seq.toList retry |> List.item 0) = 'n' then
            hasQuit <- true
        else
            hasQuit <- false
    0