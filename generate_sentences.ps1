$words = [ordered]@{
    "A" = @("alternativement", "apparemment", "alentour", "autrefois", "absolument", "ailleurs", "aujourd'hui", "aussi", "autour", "avant", "avant-hier", "apr`u{00e8}s", "aucunement")
    "B" = @("beaucoup", "bien")
    "C" = @("combien", "certainement", "compl`u{00e8}tement", "comment", "certes")
    "D" = @("d`u{00e9}sormais", "debout", "dedans", "dehors", "devant", "derri`u{00e8}re", "d`u{00e9}j`u{00e0}", "dor`u{00e9}navant", "directement")
    "E" = @("extr`u{00ea}mement", "encore", "enfin", "`u{00e9}galement", "`u{00e9}videmment")
    "F" = @("forc`u{00e9}ment", "futilement", "finalement", "fixement")
    "I" = @("imm`u{00e9}diatement")
    "J" = @("jadis", "juste", "joyeusement", "justement")
    "L" = @("loin", "lors", "lat`u{00e9}ralement", "litt`u{00e9}ralement", "l`u{00e9}g`u{00e8}rement", "largement")
    "M" = @("m`u{00ea}me", "malheureusement", "maintenant", "mieux", "moins")
    "N" = @("notamment", "n`u{00e9}anmoins")
    "P" = @("proprement", "presque", "pourtant", "pr`u{00e8}s", "parfois", "partout", "peu", "plus", "pourquoi", "puis", "parfaitement", "peut-`u{00ea}tre", "probablement", "partiellement", "pratiquement", "pr`u{00e9}c`u{00e9}demment", "pr`u{00e9}cis`u{00e9}ment", "pr`u{00e9}cieusement", "principalement", "progressivement", "publiquement", "patiemment", "prudemment", "profond`u{00e9}ment", "pond`u{00e9}r`u{00e9}ment", "purement", "passionn`u{00e9}ment", "puissamment")
    "Q" = @("quand")
    "R" = @("rapidement")
    "S" = @("super", "seulement", "souvent", "subitement", "soudain", "soigneusement")
    "T" = @("totalement", "tr`u{00e8}s", "tranquillement", "tard", "tant")
    "U" = @("uniquement")
    "V" = @("vraiment", "vite")
}

$basePath = "Sentences"
$count = 0

foreach ($letter in $words.Keys) {
    $letterPath = Join-Path $basePath $letter
    if (-not (Test-Path $letterPath)) {
        New-Item -ItemType Directory -Path $letterPath -Force | Out-Null
    }
    foreach ($word in $words[$letter]) {
        $json = "{
  ""description"": ""En fran`u{00e7}ais, l'adverbe `u{00ab} $word `u{00bb} sert `u{00e0} caract`u{00e9}riser et `u{00e0} pr`u{00e9}ciser la nature d'un nom auquel il se rapporte dans une phrase. Son usage est r`u{00e9}pandu dans divers registres de langue, du familier au soutenu, et il contribue `u{00e0} la richesse descriptive du discours."",
  ""sentences"": [
    ""L'architecte a pr`u{00e9}sent`u{00e9} son projet devant le jury, qui l'a trouv`u{00e9} $word, saluant notamment son approche originale et respectueuse de l'environnement."",
    ""Les critiques ont unanimement d`u{00e9}crit le nouveau spectacle comme `u{00e9}tant $word, soulignant la qualit`u{00e9} exceptionnelle de la mise en sc`u{00e8}ne."",
    ""En observant attentivement le comportement des participants, le chercheur a constat`u{00e9} que leur `u{00e9}tat `u{00e9}motionnel `u{00e9}tait syst`u{00e9}matiquement $word."",
    ""Le guide touristique a expliqu`u{00e9} que ce monument historique du dix-huiti`u{00e8}me si`u{00e8}cle `u{00e9}tait consid`u{00e9}r`u{00e9} comme $word par les visiteurs du monde entier."",
    ""Le photographe a su capturer un instant pr`u{00e9}cieux que les visiteurs de l'exposition ont unanimement qualifi`u{00e9} de $word.""
  ]
}"
        $filePath = Join-Path $letterPath "$word.json"
        $utf8NoBom = New-Object System.Text.UTF8Encoding $false
        [System.IO.File]::WriteAllText($filePath, $json, $utf8NoBom)
        $count++
    }
}

Write-Host "Done! Created $count JSON files."
