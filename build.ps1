param(
    # Build only A
    [switch]
    $a,
    # Build only B
    [switch]
    $b,
    # Build only C
    [switch]
    $c
)

#build all files
function BuildA {
    dotnet build .\ProccessA\ProccessA.csproj
}

function BuildB {
    dotnet build .\ProccessB\ProccessB.csproj
}

function BuildC {
    dotnet build .\ProccessC\ProccessC.csproj
}

function Main{
    if($a.IsPresent -eq $true){
        BuildA
    }
    if($b.IsPresent -eq $true){
        BuildB
    }
    if($c.IsPresent -eq $true){
        BuildC
    }
}

Main