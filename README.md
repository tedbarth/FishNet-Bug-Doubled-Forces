# FishNet-Bug-Doubled-Forces
This is an example where I found a weird bug and I don't know if it caused by FishNet, Unity or myself.

This example uses CSP to predict the Player's objects position, rotation and velocities. Because of unknown reasons the client always reacts twice as strong (or more) to the inputs, when connected to a server. A Host (Client and Server) does not have this problem. To see the problem I [deactivated](https://github.com/tedbarth/FishNet-Bug-Doubled-Forces/blob/main/Assets/Scripts/PlayerCspController.cs#L69) the reconciliation by just not applying the absolute state coming from server. If you activate it again, the bug is still there, but less visible: You would see the client jumping on every rotation direction change. 

<iframe width="560" height="315" src="https://www.youtube.com/embed/lZ0gXDKxd9U" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

# Reproduction
- Start two instances (e.g. use the tool Project Clone)
- In one instance press 0 to start a server
- In one instance press 9 to start a client
- Rotate the player object using left/right arrow keys