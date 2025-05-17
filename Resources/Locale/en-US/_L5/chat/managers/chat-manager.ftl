# Sign language
chat-manager-entity-sign-wrap-message = { PROPER($entity) ->
*[false] the {$entityName} signs, [color={$color}][italic]{$message}[/italic][/color]
[true] {$entityName} signs, [color={$color}][italic]{$message}[/italic][/color]
    }

chat-manager-entity-unknown-sign-wrap-message = { PROPER($entity) ->
*[false] [color={$color}]the {$entityName} gestures something.[/color]
[true] [color={$color}]{$entityName} gestures something.[/color]
    }

hud-chatbox-select-channel-Sign = Sign
chat-manager-entity-sign-no-free-hands = You need at least one free hand to sign!
