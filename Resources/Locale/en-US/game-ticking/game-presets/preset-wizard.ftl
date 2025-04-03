## Survivor

roles-antag-survivor-name = Survivor
# It's a Halo reference
roles-antag-survivor-objective = Current Objective: Survive

# L5
survivor-role-greeting =
    You are a Survivor.
    Above all you need to make it back to Central Admin alive.
    Collect as much firepower as needed to guarantee your survival.
    Trust no one.

survivor-round-end-dead-count =
{
    $deadCount ->
        [one] [color=red]{$deadCount}[/color] survivor died.
        *[other] [color=red]{$deadCount}[/color] survivors died.
}

survivor-round-end-alive-count =
{
    $aliveCount ->
        [one] [color=yellow]{$aliveCount}[/color] survivor was marooned on the station.
        *[other] [color=yellow]{$aliveCount}[/color] survivors were marooned on the station.
}

survivor-round-end-alive-on-shuttle-count =
{
    $aliveCount ->
        [one] [color=green]{$aliveCount}[/color] survivor made it out alive.
        *[other] [color=green]{$aliveCount}[/color] survivors made it out alive.
}

## Wizard
# TODO: L5: do we want wizard? We can say they're sufficiently-advanced?

objective-issuer-swf = [color=turquoise]The Space Wizards Federation[/color]

wizard-title = Wizard
wizard-description = There's a Wizard on the station! You never know what they might do.

roles-antag-wizard-name = Wizard
roles-antag-wizard-objective = Teach them a lesson they'll never forget.

# L5
wizard-role-greeting =
    You're a wizard, you sufficently-advanced bastard!
    There's been tensions between the Space Wizards Federation and the System Consortium.
    So you've been selected by the Space Wizards Federation to pay a visit to the station.
    Give them a good demonstration of your powers.
    What you do is up to you, just remember the Space Wizards want you to make it out alive.

wizard-round-end-name = wizard

## TODO: Wizard Apprentice (Coming sometime post-wizard release)
