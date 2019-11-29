using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextController : MonoBehaviour {

	public Text text;
	private string textHolder;
	private enum States {
		start,
		cell, bed, cloth, mirror, latrine, smellyHand, closedDoor, openDoor,
		corridor, otherCells, stairs, closedCloset, findKey, chasingBug, openCloset, jammedDoor, 
		courtyard, trashBin, cellWings, scavenging, refectory, washroom, towerDoor, 
		tower, radio, listeningToRadio, controls, siren, openExitDoor, blockDoors, landscape, drawer, cantExitTower,
		sinks, showers, towel, tile, toilets, openCistern, coins, washingHands, takingShower,
		poster, tables, vendingMachine, food, sick, kitchen, coldRoom, deadGuard, findTowerKey, bench,
		exitDoor, dogCorridor, passingTheDog, dogAssault, dogFeed}
	private States myState;
	private bool started;
	private bool outcome;
	// cell
	private bool sawLocked;
	private bool sawMirror;
	private bool sawSpring;
	private bool sawLatrine;
	private bool hasSmellyHand;
	private bool hasCloth;
	private bool hasSpring;
	private bool doorOpen;
	// corridor
	private bool sawCorridor;
	private bool sawOtherCells;
	private bool sawCloset;
	private bool closetOpen;
	private bool hasCrowbar;
	private bool sawJammed;
	// courtyard and premisses
	private bool sawCourtyard;
	private bool sawCellWings;
	private bool scavenged;
	private bool sawRefectory;
	private bool sawWashroom;
	private bool sawTowerDoor;
	private bool hasTowerKey;
	private bool sawTower;
	// tower
	private bool sawLandscape;
	private bool sawInsideDrawer;
	private bool hasControlKey;
	private bool sirenWentOff;
	private bool doorsBlocked;
	private bool exitOpen;
	// washroom
	private bool hasSoap;
	private bool hasTowel;
	private bool tookShower;
	private bool sawToilets;
	private bool sawInsideCistern;
	// refectory
	private int sawTables;
	private bool hasCoins;
	private bool sawVendingMachine;
	private List<string> foodBought;
	private bool ate;
	private bool isSick;
	private bool wasSick;
	private bool sawKitchen;
	private bool sawBench;
	private bool hasKnife;
	private bool sawColdRoom;
	private bool deadGuardOut;
	private bool sawExitClosed;
	// dog
	private bool sawDog;
	private bool theEnd;
	
	private string[] cellThoughts;
	private string[] corridorThoughts;
	private string[] courtyardThoughts;
	private string[] washroomThoughts;
	private string[] refectoryThoughts;
	private string[] kitchenThoughts;
	private string[] radioNoise;
	private int randomIndex;
	
	// Use this for initialization
	void Start () {
		textHolder = "";
		myState = States.start;
		started = false;
		// cell
		sawLocked = false;
		sawMirror = false;
		sawSpring = false;
		sawLatrine = false;
		hasSmellyHand = false;
		hasCloth = false;
		hasSpring = false;
		doorOpen = false;
		// corridor
		sawCorridor = false;
		sawOtherCells = false;
		sawCloset = false;
		closetOpen = false;
		hasCrowbar = false;
		sawJammed = false;
		// courtyard
		sawCourtyard = false;
		sawCellWings = false;
		scavenged = false;
		sawRefectory = false;
		sawWashroom = false;
		sawTowerDoor = false;
		hasTowerKey = false;
		sawTower = false;
		// tower
		sawLandscape = false;
		sawInsideDrawer = false;
		hasControlKey = false;
		sirenWentOff = false;
		doorsBlocked = false;
		exitOpen = false;
		// washroom
		hasSoap = false;
		hasTowel = false;
		tookShower = false;
		sawToilets = false;
		sawInsideCistern = false;
		// refectory
		sawTables = 0;
		hasCoins = false;
		sawVendingMachine = false;
		foodBought = new List<string>();
		ate = false;
		isSick = false;
		wasSick = false;
		sawKitchen = false;
		sawBench = false;
		hasKnife = false;
		sawColdRoom = false;
		deadGuardOut = false;
		sawExitClosed = false;
		sawDog = false;
		theEnd = false;
		
		cellThoughts = new string[] {"This cell appears to be shrinking...", 
									"You can't hear nothing outside this room...",
									"You are starting to feel numb...",
									"Your heart suddenly races!... apparently without any reason...",
									"You wonder if you are in some kind of prison...",
									"Your hungry stomach churns like a cat in heat!"};
		
		corridorThoughts = new string[] {"Walking along the corridor, you only hear the ecoes of your footsteps. No other signs of life in here.", 
										"What was that? You think you saw something moving in the darkness inside a cell...",
										"The silence is terrifying.",
										"You are tempted to scream out loud only to see if someone responds.",
										"You ask yourself why would anyone lock you up in here.",
										"You are tired of walking around this corridor..."};
		
		courtyardThoughts = new string[] {"While standing in the middle of the courtyard, you look up to the grey sky.", 
										"You walk around the courtyard, but don't find anything that you didn't see before.",
										"You imagine the profound boredom felt by prisoners when confined in this courtyard.",
										"Could you be able to somehow climb the courtyard walls? Nah...",
										"Hmmmm, you study the possibility of digging up an exit from the courtyard ground. Nah...",
										"You are tired of walking around this courtyard..."};
		
		washroomThoughts = new string[] {"The washroom air is charged with humidity and smells moldy.", 
										"What was that?\nThe echo inside the washroom confuses you a little...",
										"Wow, you were reached by a terrible smell coming from the toilets area.",
										"You wonder what a clean folded towel was doing hangin' in a place like this.",
										"You ask yourself what are you doing in this washroom, given that the exit is definitely NOT here!",
										"Uops! You almost fall down while walking around this slippery moisty floor."};
		
		refectoryThoughts = new string[] {"The refectory is completely cleaned and ordered. Quite strange under the circunstances...", 
										"All of sudden, the refectory seems much larger than before.",
										"You catch yourself imagining the bad but-nevertheless-edible food they would serve in here.",
										"Wow!!! Did you just performed a dancing step while crossing the room?",
										"Tired of walking around, you sit in a table for a couple of minutes while thinking your next move.",
										"Meditating for a little bit, you can't comprehend how nothing in this place seems familiar to you." };
		
		kitchenThoughts = new string[] {"The hygiene of this kitchen surprises you.", 
										"Oh, if only you had some ingredients... You could make some pancakes!",
										"The kitchen is very cold...",
										"'Let it gooo, let it gooo'... You are surprised that you DO remember this song.",
										"WOW! You think you just heard something back in the refectory!!!",
										"You egarly continue to look all around the kitchen, but there is no trace of food..." };
		
		radioNoise = new string[] {"blurp", "kikikikiki", "hummmmmm", "rrrrrover", "bzzzzzz", "da....ger...repit...",
									"calling all", "shhhhhhhh", "anyone?", "wonwonwonwonwon", "blip", "AAAARGH!!!",
									"....", "lone", "WHAT TH", "hmmmmm--blurp", "bip", "102F", "hahaha", "TCHT", "UBIUBIUBI"};
		
		reset_randomIndex(cellThoughts);
	}
	
	// Update is called once per frame
	void Update () {
		// cell
		if 		(myState == States.start)			{startState();} 
		else if (myState == States.cell) 			{cell();} 
		else if (myState == States.cloth) 			{cloth();} 
		else if (myState == States.bed) 			{bed();} 
		else if (myState == States.mirror) 			{mirror();} 
		else if (myState == States.latrine) 		{latrine();} 
		else if (myState == States.smellyHand) 		{smellyHand();} 
		else if (myState == States.closedDoor) 		{closedDoor();} 
		else if (myState == States.openDoor) 		{openDoor();}
		// corridor
		else if (myState == States.corridor) 		{corridor();}
		else if (myState == States.otherCells) 		{otherCells();} 
		else if (myState == States.stairs) 			{stairs();} 
		else if (myState == States.closedCloset) 	{closedCloset();}
		else if (myState == States.findKey) 		{findKey();}
		else if (myState == States.openCloset) 		{openCloset();}
		else if (myState == States.chasingBug) 		{chasingBug();}
		else if (myState == States.jammedDoor) 		{jammedDoor();}
		// courtyard
		else if (myState == States.courtyard) 		{courtyard();}
		else if (myState == States.towerDoor) 		{towerDoor();}
		else if (myState == States.cellWings) 		{cellWings();}
		else if (myState == States.trashBin) 		{trashBin();}
		else if (myState == States.scavenging) 		{scavenging();}
		// tower
		else if (myState == States.tower) 			{tower();}
		else if (myState == States.landscape) 		{landscape();}
		else if (myState == States.radio) 			{radio();}
		else if (myState == States.listeningToRadio) {listeningToRadio();}
		else if (myState == States.drawer) 			{drawer();}
		else if (myState == States.controls) 		{controls();}
		else if (myState == States.siren) 			{siren();}
		else if (myState == States.blockDoors) 		{blockDoors();}
		else if (myState == States.openExitDoor) 	{openExitDoor();}
		else if (myState == States.cantExitTower) 	{cantExitTower();}
		// washroom
		else if (myState == States.washroom)	 	{washroom();}
		else if (myState == States.sinks) 			{sinks();}
		else if (myState == States.showers) 		{showers();}
		else if (myState == States.towel) 			{towel();}
		else if (myState == States.tile) 			{tile();}
		else if (myState == States.toilets) 		{toilets();}
		else if (myState == States.openCistern) 	{openCistern();}
		else if (myState == States.coins) 			{coins();}
		else if (myState == States.washingHands) 	{washingHands();}
		else if (myState == States.takingShower) 	{takingShower();}
		// refectory
		else if (myState == States.refectory)	 	{refectory();}
		else if (myState == States.poster) 			{poster();}
		else if (myState == States.tables) 			{tables();}
		else if (myState == States.vendingMachine) 	{vendingMachine();}
		else if (myState == States.food) 			{food();}
		else if (myState == States.sick) 			{sick();}
		else if (myState == States.kitchen) 		{kitchen();}
		else if (myState == States.coldRoom) 		{coldRoom();}
		else if (myState == States.deadGuard) 		{deadGuard();}
		else if (myState == States.findTowerKey) 	{findTowerKey();}
		else if (myState == States.bench)		 	{bench();}
		else if (myState == States.exitDoor)		{exitDoor();}
		// dog corridor
		else if (myState == States.dogCorridor)		{dogCorridor();}
		else if (myState == States.passingTheDog)	{passingTheDog();}
		else if (myState == States.dogAssault)		{dogAssault();}
		else if (myState == States.dogFeed)			{dogFeed();}
		
		text.text = textHolder;
	}
	
	void startState () {
		textHolder = "\n\nWelcome to the 'Escape the PRISON', an adventure text game!\n\n\nPress RETURN to start.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.cell;
		}
	}
	
	void cell () {
		
		if (!started) {
			textHolder = "You wake up in a strange bed. Nauseous. " + 
						"You can't remember anything. Not even your name. " +
						"You find yourself surrounded by concrete walls inside some kind " + 
						"of prison cell. A flickering light barely illuminates the room. " + 
						"You see a piece of dirty cloth on the floor and a broken mirror " + 
						"hanging on the wall over a smelly latrine. The only door is just a " + 
						"couple of meters from the bed. It is a metal door which seems quite solid. " +
						"The air is suffocating and you feel the urge to get out.\n";
		} else {
			textHolder = cellThoughts[randomIndex];
			textHolder += "\n\nShall you ";
			if (!sawMirror) {textHolder += "check the mirror? ";} 
			else {textHolder += "look again at the mirror? ";}
			if (!sawLatrine) {textHolder += "Or does the latrine raise your interest? ";} 
			else {
				if (!hasSmellyHand) {textHolder += "Or does the latrine still puzzle you? ";}
			}
			if (!hasCloth) {textHolder += "Or will you get the dirty cloth from the floor? ";}
			if (!sawSpring) {textHolder += "Or maybe examine the bed you were just in? " ;} 
			else {
				if (!hasSpring) {textHolder += "Or examine the bed again? ";}
			}
			if (!sawLocked) {textHolder += "Or will you go for the door?";} 
			else {
				if (!doorOpen) {textHolder += "Or will you check the door again?";} 
				else {textHolder += "Or will you go to the door?";}
			}
			textHolder += "\n";
		}
		
		textHolder += "\nPress ";
		if (!hasSpring) {textHolder += "B to examine the bed, ";}
		if (!hasCloth) {textHolder += "C to get the piece of cloth, ";}
		textHolder += "M to check the mirror";
		if (hasSpring && hasSmellyHand) {textHolder += " ";} 
		else {textHolder += ", ";}
		if (!hasSmellyHand) {textHolder += "L to investigate the latrine, ";}
		textHolder += "or D to try out the door.";
		
		if (Input.GetKeyDown(KeyCode.B) && !hasSpring){
			myState = States.bed;
			started = true;
			reset_randomIndex(cellThoughts);
		} else if (Input.GetKeyDown(KeyCode.C) && !hasCloth){
			myState = States.cloth;
			started = true;
			reset_randomIndex(cellThoughts);
		} else if (Input.GetKeyDown(KeyCode.M)){
			myState = States.mirror;
			started = true;
			reset_randomIndex(cellThoughts);
		} else if (Input.GetKeyDown(KeyCode.L) && !hasSmellyHand){
			myState = States.latrine;
			started = true;
			reset_randomIndex(cellThoughts);
		} else if (Input.GetKeyDown(KeyCode.D)){
			if (!doorOpen) {myState = States.closedDoor;} 
			else {myState = States.openDoor;}
			started = true;
			reset_randomIndex(cellThoughts);
		}
		
	}
	
	void reset_randomIndex (string[] textList) {
		randomIndex = Random.Range(0, textList.Length);
	}
	
	void cloth () {
		
		textHolder = "The piece of cloth is completly ruined!" + 
					"\n\nPress T to take the cloth, or RETURN to leave it.";
		
		if (Input.GetKeyDown(KeyCode.T)){
			myState = States.cell;
			hasCloth = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.cell;
		}
	}
	
	void bed () {
		
		if (!sawSpring) {
			textHolder = "You examine the bed. It is covered by the smelly sheets " + 
						"in which you were sleeping in. Pushing away the sheets you find that the mattress " +
						"has a spring coming out of it. ";
		} else {
			textHolder = "You examine the bed again.\n\nYou find nothing besides " +
						"the sharp spring standing out of the mattress surface. "; 
		}
		
		if (!hasCloth) {
			textHolder += "It is quite sharp, so you will need " +
						"something to protect your hands in order to break it out." + 
						"\n\nPress RETURN.";
		} else {
			textHolder += "You can use the piece of cloth to wrap " +
						"your hand with and try to break the spring out." + 
						"\n\nPress T to take the spring, or RETURN to leave it.";
			if (Input.GetKeyDown(KeyCode.T)) {
				sawSpring = true;
				hasSpring = true;
				myState = States.cell;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.cell;
			sawSpring = true;
		}
	}
	
	void mirror () {
		
		if (!sawMirror) {
			textHolder = "Although it is broken, the mirror reveals your mysterious face to you. " +
						"It is completely unfamiliar and you have a deep feeling of emptiness...";
		} else {
			textHolder = "Every gaze at the mirror makes you a little sicker than before...";
		}
		textHolder += "\n\nPress RETURN to stop looking at it.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.cell;
			sawMirror = true;
		}
		
	}
	
	void latrine () {
		
		if (!sawLatrine) {
			textHolder = "The latrine has something inside it... It is so grubby that " +
						"you can't see clearly what it is.";
		} else {
			textHolder = "You are sure that there is something deep inside this latrine...";
		}
		textHolder += "\n\nPress H to put your hand on it or RETURN to leave it.";
		
		if (Input.GetKeyDown(KeyCode.H)){
			sawLatrine = true;
			hasSmellyHand = true;
			myState = States.smellyHand;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			sawLatrine = true;
			myState = States.cell;
		}
	}
	
	void smellyHand () {
		
		textHolder = "You dive your hand into the dark hole..." +
					"\nOooh!!!\nYeap, it is just what you would expect to be in a latrine... " +
					"\nYou now have a smelly hand. Great!" + 
					"\n\nPress RETURN to leave it in shame.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.cell;
		}
			
	}
	
	void closedDoor () {
		
		if (!sawLocked) {
			textHolder = "The door is locked. Doesn't surprise you." +
						" The key hole is exposed from the inside though. You ";
		} else {
			textHolder = "The door is locked, but you ";
		}
		
		if (!hasSpring) {
			textHolder += "could try to pick the lock with something sharp." + 
						"\n\nPress RETURN to continue inspecting the room.";
		} else {
			textHolder += "could try to pick the lock with the spring that you just " +
						"took from the mattress." +
						"\n\nPress P to pick the lock or RETURN to continue inspecting the room.";
		}
			
		if (Input.GetKeyDown(KeyCode.P) && hasSpring){
			sawLocked = true;
			myState = States.openDoor;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			sawLocked = true;
			myState = States.cell;
		}
	}
		
	void openDoor () {
		
		if (!doorOpen) {
			textHolder = "You hear a clocking sound inside the lock...\nThe door is now open!!!";
		} else {
			textHolder = "The door is open, but you still can't see much outside...";
		}
		textHolder += "\n\nPress E to exit the cell, " + 
					"or RETURN to continue inspecting the room.";
			
		if (Input.GetKeyDown(KeyCode.E)){
			doorOpen = true;
			myState = States.corridor;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			doorOpen = true;
			myState = States.cell;
		}		
	}
	
	void corridor () {
		
		if (!sawCorridor) {
			textHolder = "You see a dark corridor lightened only by your cell's flickering light. " +
						"There are other cells along the corridor. At the end you can see an open door leading to a staircase up. " + 
						"Could that be the exit? You also see a cleaning closet at your right.";
		} else {
			textHolder = corridorThoughts[randomIndex];
		}
		textHolder += "\n\nWhat will you do?\nWill you ";
		
		if (!sawOtherCells) {
			textHolder += "look around and check the other cells? ";
		} else {
			textHolder += "double-check the cells, in case you missed something? ";
		}
		
		if (!sawCloset) {
			textHolder += "Will you search the closet instead? ";
		} else {
			if (!hasCrowbar) {
				textHolder += "Or visit the closet again? ";
			}
		}
		if (!sawJammed) {
			textHolder += "Or go straight for the possible exit upstairs?";
		} else {
			textHolder += "Or Try your luck again against the jammed door upstairs?";
		}
		
		textHolder += "\n\nPress L to look into other cells"; 
		if (!hasCrowbar) {textHolder += ", C to search the closet";}
		textHolder += " or S for the stairs.";
		
		if (Input.GetKeyDown(KeyCode.S)){
			sawCorridor = true;
			myState = States.stairs;
			reset_randomIndex(corridorThoughts);
		} else if (Input.GetKeyDown(KeyCode.L)){
			sawCorridor = true;
			myState = States.otherCells;
			reset_randomIndex(corridorThoughts);
		} else if (Input.GetKeyDown(KeyCode.C) && !hasCrowbar){
			sawCorridor = true;
			if (!closetOpen) {myState = States.closedCloset;}
			else {myState = States.openCloset;}
			reset_randomIndex(corridorThoughts);
		}
	}
	
	void otherCells () {
		if (!sawOtherCells) {
			textHolder = "You go over every cell door throughout the corridor. Although some are closed, they all seem to be empty. " +
						"No windows, so you are probably underground. You didn't find anything useful.";
		} else {
			textHolder = "You look even harder, going through all dark corners of every open cell...\nBut still nothing!";
		}
		textHolder += "\n\nPress RETURN to give up your meaningless task.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
			sawOtherCells = true;
		}
	}
	
	void closedCloset () {
		if (!sawCloset) {
			textHolder = "You try to open the closet's door, only to find that it is locked.\n\n";
		} else {
			textHolder = "The closet's door is still locked.\n\n";
		}
		textHolder += "Do you want to search around the closet's door or just leave the closet as it is?\n\n";
		textHolder += "Press S to search or RETURN to leave it.";
		
		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.findKey;
			sawCloset = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
			sawCloset = true;
		}
		
	}
	
	void findKey () {
		textHolder = "Searching around the closet's door you find a small key hidden in the door frame. " +
					"You take it hoping that you can now open the closet.\n\nPress RETURN to use the key in the closet's door.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.openCloset;
		}
	}
	
	void openCloset () {
		if (!closetOpen) {
			textHolder = "You open the closet's door.\nSuddenly a cockroach carrying a cigarette on its back crawls " + 
					"outside the closet and runs towards the stairs. Looks like you aren't alone after all! " +
					"Inside the closet you find out-of-date cleaning products and junk gallore. Buried among them " +
					"you find a crowbar that will certainly come in handy.\nDo you pursue the cockroach in its way towards freedom? Or do ";
		} else {
			textHolder = "You contemplate the mountain of trash that was compacted into the closet and finally understand " +
				"the state of your latrine. ";
			if (!hasCrowbar) {textHolder += "The crowbar is still there...\nDo ";}
		}
		textHolder += "you take the crowbar?\n\nPress ";
		if (!closetOpen) {textHolder += "P to pursue the cockroach, ";}
		if (!hasCrowbar) {textHolder += "T to take the crowbar and leave or ";}
		textHolder +="RETURN to leave the closet.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
			closetOpen = true;
		} else if (Input.GetKeyDown(KeyCode.P) && !closetOpen){
			myState = States.chasingBug;
			closetOpen = true;
		} else if (Input.GetKeyDown(KeyCode.T) && !hasCrowbar){
			myState = States.corridor;
			closetOpen = true;
			hasCrowbar = true;
		}
	}
	
	void chasingBug () {
		textHolder = "You run after the cockroach, but it is too fast for you. Plus, " +
					"it is quite dark and you quickly loose track of the insect. It is indeed a survivor!\n\n" +
					"You find yourself at the middle of the corridor.\n\nPress RETURN to continue.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
		}
	}
	
	void stairs () {
		textHolder = "You walk towards the end of the corridor. The staircase ";
		if (!sawJammed) {textHolder += "seems to be slightly illuminated by daylight.";}
		else {textHolder += "will lead you back to the jammed door.";}
		textHolder += "\n\nPress U to go up the stairs or RETURN to go back to the corridor.";
		
		if (Input.GetKeyDown(KeyCode.U)){
			myState = States.jammedDoor;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
		}
	}
	
	void jammedDoor () {
		textHolder = "";
		if (!sawJammed) {textHolder += "After climbing two levels of stairs, you see a heavy door wide shut. " +
									"However, you clearly see daylight through the doorway. This is definitely your way out. ";}
		textHolder += "You try to push the door, but it seems to be jammed and the handle is broken. " +
						"Your best option is to use something for leverage. ";
		if (hasCrowbar) {
			textHolder += "Maybe the crowbar is useful here.";
		}
		else {textHolder += "Could still be something useful down there?";}
		textHolder += "\n\nPress ";
		if (hasCrowbar) {textHolder += "U to use the crowbar to open the door or ";}
		textHolder += "RETURN to go back down";
		if (hasCrowbar) {textHolder += ", in case you forgot something.";} {textHolder += ".";}
		
		if (Input.GetKeyDown(KeyCode.U) && hasCrowbar){
			myState = States.courtyard;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.corridor;
			sawJammed = true;
		}
	}
	
	void courtyard () {
		if (!sawCourtyard) {
			textHolder = "You find yourself in a squared courtyard. It is surrounded by a high plain concrete wall on two of the four sides. " +
				"You cannot see what is behind it and the sky is as grey as the walls. At your right side, next to the door you just came out, " +
				"there is another door, brownish, that seems to be highly secured with an electronic lock. " +
				"Left of you there is a redish double door. On the right there are two other doors. " +
				"The closest one to you has an ocher color with water stains. The other one is greenish and is located at the farthest corner of the courtyard. Both of them " +
				"are only half-closed. You also notice a trash bin attached to the same wall, in between the ocher and greenish door.\n";
		} else {
			textHolder = courtyardThoughts[randomIndex];
			textHolder += "\n";
		}
		if (!sawTowerDoor) {textHolder += "Do you go to the brownish secured door? ";}
		else {textHolder += "Do you go to the tower's door? ";}
		if (!sawCellWings) {textHolder += "Or will you approach the wider reddish one? ";}
		else {textHolder += "Or continue studying the debris blocking the access to the cell wings? ";}
		if (!sawWashroom) {textHolder += "Maybe try the ocher moisted door? ";}
		else {textHolder += "Or maybe visit the washroom again? ";}
		if (!sawRefectory) {textHolder += "Or did the greenish door get your attention? ";}
		else {textHolder += "Or go back to the refectory? ";}
		if (!scavenged) {textHolder += "What about a little bit of scavenging by the trash bin?";}
		else {textHolder += "Or maybe you do more scavenging at the trash bin?";}
		textHolder += "\n\nPress ";
		if (!sawTowerDoor) {textHolder += "B for the brownish door, ";}
		else {textHolder += "B for the tower's door, ";}
		if (!sawCellWings) {textHolder += "R for the reddish door, ";}
		else {textHolder += "R for the debris at the cell wings' corridor, " ;}
		if (!sawWashroom) {textHolder += "O for the ocher door, ";}
		else {textHolder += "O for the washroom's door, ";}
		if (!sawRefectory) {textHolder += "G for the greenish door, ";}
		else {textHolder += "G for the refectory's door, ";}
		if (!scavenged) {textHolder += "or T for the trash bin.";}
		else {textHolder += "or T for the exploring the trash bin again.";}
		
		if (Input.GetKeyDown(KeyCode.B)){
			myState = States.towerDoor;
			sawCourtyard = true;
			reset_randomIndex(courtyardThoughts);
		} else if (Input.GetKeyDown(KeyCode.R)){
			myState = States.cellWings;
			sawCourtyard = true;
			reset_randomIndex(courtyardThoughts);
		} else if (Input.GetKeyDown(KeyCode.O)){
			myState = States.washroom;
			sawCourtyard = true;
			reset_randomIndex(washroomThoughts);
		} else if (Input.GetKeyDown(KeyCode.G)){
			myState = States.refectory;
			sawCourtyard = true;
			reset_randomIndex(refectoryThoughts);
		} else if (Input.GetKeyDown(KeyCode.T)){
			myState = States.trashBin;
			sawCourtyard = true;
			reset_randomIndex(courtyardThoughts);
		}
		
	}
	
	void cellWings () {
		if (!sawCellWings) {textHolder = "Once you are close to the reddish door, you realize it is completely blocked by various debris. All evidence points to " +
										"a now extinguished prison rebellion. You wonder what happened...\nA sign reads 'Cell wings'. " +
										"There is no way you can unblock the entrance.";}
		else {textHolder = "The cell wings are unreacheable from the courtyard.";}
		textHolder += "\n\nPress RETURN to continue your walk through the courtyard.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.courtyard;
			sawCellWings = true;
		}
	}
	
	void trashBin () {
		textHolder = "The trash bin is completely full. As far as you can see, most of it is used napkins and snack packaging. " +
					"Could it have anything useful beneath?\n\nPress S to scavenge or RETURN to leave it.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.courtyard;
		}
		if (Input.GetKeyDown(KeyCode.S)){
			myState = States.scavenging;
		}
	}
	
	void scavenging () {
		textHolder = "You search among the trash, but don't find anything besides the napkins and packaging. ";
		if (!hasSmellyHand) {textHolder += "\nOw! You now have a smelly hand...";}
		else {textHolder += "\nWell, it kind of washed off some of the stink that you hand got from before!";}
		textHolder += "\n\nPress RETURN to do something useful instead!";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.courtyard;
			scavenged = true;
			hasSmellyHand = true;
		}
	}
	
	void towerDoor () {
		if (!sawTowerDoor) {
			textHolder = "There is a small sign that reads 'Tower'. This door is heavely secured. " +
						"You will need some kind of key card to open it.";
			if (hasTowerKey) {textHolder += "\nYou use the key card you took from the guard pocket in the door console. " +
											"After a blip sound the door opens inwards, revealing spiral staircase going up." +
											"\nDo you go up?";}
		} else {
			if (!sawTower) {
				if (!hasTowerKey) {textHolder = "You need some kind of key card to open this door.";;}
				else {textHolder = "You use the key card you took from the guard pocket in the door console. " +
									"After a blip sound the door opens inwards, revealing spiral staircase going up." +
									"\nDo you go up?";}
			} else {textHolder = "You use the key card to open the door.";}
		}
		textHolder += "\n\nPress ";
		if (hasTowerKey) {textHolder += "U to go up the stairs, or ";}
		textHolder += "RETURN to turn away towards the courtyard.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.courtyard;
			sawTowerDoor = true;
		}
		if (Input.GetKeyDown(KeyCode.U) && hasTowerKey){
			myState = States.tower;
			sawTowerDoor = true;
		}
	}
	
	void tower () {
		if (!sawTower) {textHolder = "After climbing several floors, you arrive into a small one-person security station. There is no guard. " +
									"The exterior landscape is visible through windows in all directions. In front of the guard's chair there is " +
									"a control panel and a radio switched off. Beneath it, you see a drawer.";}
		else {
			//add random thoughts
			textHolder = "Standing next to the tower's security station, you gaze at the family photos covering the walls, just beneath the windows. ";
		}
		textHolder += "\nWhat will you do now? Do you contemplate the landscape? Check the control panel? Turn the radio on? ";
		if (!hasControlKey) {textHolder += "Check what is inside the drawer? ";}
		textHolder += " Or will you go back to the courtyard?\n\nPress L to contemplate the landscape, C to examine the controls, " +
					"R to turn on the radio";
		if (!hasControlKey) {textHolder += ", D to open the drawer";}
		textHolder += ", or RETURN to climb down the stairs.";
		
		if (Input.GetKeyDown(KeyCode.L)){
			myState = States.landscape;
			sawTower = true;
		} else if (Input.GetKeyDown(KeyCode.C)){
			myState = States.controls;
			sawTower = true;
		} else if (Input.GetKeyDown(KeyCode.R)){
			myState = States.radio;
			sawTower = true;
		} else if (Input.GetKeyDown(KeyCode.D) && !hasControlKey){
			myState = States.drawer;
			sawTower = true;
		} else if (Input.GetKeyDown(KeyCode.Return) && !doorsBlocked){
			myState = States.courtyard;
			sawTower = true;
			reset_randomIndex(courtyardThoughts);
		} else if (Input.GetKeyDown(KeyCode.Return) && doorsBlocked){
			myState = States.cantExitTower;
			sawTower = true;
		}
	}
	
	void landscape () {
		if (!sawLandscape) {textHolder = "From here you can see that you are indeed inside a prison. There is forest all around it. " +
							"On one side you observe a pillar of smoke rising from beyond the trees. On the opposite side, relatively far from you, " +
							"there seems to be a lake. The entire horizon is covered by a mysterious fog.\nMore importantly, you observe that " +
							"the area of the courtyard, ";}
		else {textHolder = "As you observed before, you cannot escape this prison from the courtyard, ";}
		if (!sawCellWings) {textHolder += "the large part of the building that is behind the reddish door, ";}
		else {textHolder += "the whole of the cell wings, ";}
		if (!sawLandscape) {textHolder += "and the tower itself are completely isolated from the outside. However, " +
							"you do see an access area behind the rest of the prison, towards ";}
		else {textHolder += "or the tower you are in. There is however a visible way out somewhere beyond ";}
		if (!sawWashroom) {textHolder += "the ocher door ";}
		else {textHolder += "the washroom ";}
		textHolder += "and the refectory.\n\nPress RETURN to interrupt this insightful moment.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.tower;
			sawLandscape = true;
		}
	}
	
	void radio () {
		textHolder = "You switch on the radio and listen...\n\n";
		textHolder += ".....";
		reset_randomIndex(radioNoise);
		for (int i = 0; i < 20; i++){
			textHolder += radioNoise[randomIndex];
			reset_randomIndex(radioNoise);
			textHolder += ".....";
		}
		textHolder += "\n\nHearing this is making you nervous...\n" +
					"You say something yourself through the microphone, " +
					"but there is no response.\n\nPress RETURN to switch it off.";
		myState = States.listeningToRadio;
	}
	
	void listeningToRadio () {
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.tower;
		}
	}
	
	void drawer () {
		if (!sawInsideDrawer) {textHolder = "You open the drawer and see it is full of dirty magazines. Under them you find a key ring with two small keys.";}
		else {textHolder = "You see the key ring under the dirty magazines.";}
		textHolder += "\nDo you grab it?\n\nPress T to take the key set or RETURN to close the drawer.";
		if (Input.GetKeyDown(KeyCode.T)){
			myState = States.tower;
			hasControlKey = true;
			sawInsideDrawer = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.tower;
			sawInsideDrawer = true;
		}
	}
	
	void controls () {
		textHolder = "The control panel is quite simple, though there are no instructions whatsoever. " +
			"It has one big red button on the left and two key holes on the right, arranged verticaly.\nWhat will you do now?\n";
		if (!sirenWentOff) {textHolder += "Press the tempting big red button? ";}
		else {textHolder += "Play the siren again? ";}
		if (hasControlKey) {
			if (!doorsBlocked) {textHolder += "Use a key you found at the drawer on the upper key hole? ";}
			else {textHolder += "Deactivate the upper key hole mechanism? ";}
			if (!exitOpen && hasControlKey) {textHolder += "Or try the bottom key hole instead? ";}
			else {textHolder += "Or turn off the bottom key hole? ";}
		} else {textHolder += "You obviously cannot do anything with the panel key holes...";}
		
		textHolder += "\n\nPress R ";
		if (!sirenWentOff) {textHolder += "for the big red button, ";}
		else {textHolder += "to hear the siren again, ";}
		if (hasControlKey) {
			if (!doorsBlocked) {textHolder += "U to activate the upper key hole, ";}
			else {textHolder += "U to deactivate the upper key hole, ";}
			if (!exitOpen) {textHolder += "B to activate the bottom key hole or ";}
			else {textHolder += "B to deactivate the bottom key hole or ";}
		}
		else {textHolder += " or ";}
		textHolder += "RETURN to leave the panel as it is.";
		
		if (Input.GetKeyDown(KeyCode.R)){
			myState = States.siren;
		} else if (Input.GetKeyDown(KeyCode.U) && hasControlKey){
			myState = States.blockDoors;
		} else if (Input.GetKeyDown(KeyCode.B) && hasControlKey){
			myState = States.openExitDoor;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.tower;
		}
	}
	
	void siren () {
		textHolder = "";
		for (int i = 0; i < 10; i++) {textHolder += "WEEEEEEEEEEEEEEEEEE";}
		textHolder += "\n\n";
		if (!sirenWentOff) {textHolder += "Wow! You almost got a heart attack! A strident siren is off resonating throughout the whole prison. " +
										"If anyone is still around, they now can feel a little bit less alone. Hope they want to be friends with me! (Sigh)";}
		else {textHolder += "Making loud noises! It never gets old!";}
		textHolder += "\n\nPress RETURN to stop the siren.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.controls;
			sirenWentOff = true;
		}
	}
	
	void blockDoors () {
		textHolder = "You turn the key and hear a loud clocking echoing from downstairs.\n\nPress RETURN.";
		if (Input.GetKeyDown(KeyCode.Return)){
			doorsBlocked = !doorsBlocked;
			myState = States.controls;
		}
	}
	
	void cantExitTower () {
		textHolder = "You descent the stairs until the tower security door, but it won't open...\n" +
					"You are stuck inside the tower!!!\nYou climb up the stairs again.\n\nPress RETURN.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.tower;
		}
	}
	
	void openExitDoor () {
		textHolder = "You turn the key and hear nothing. You ask yourself if this had any consequence at all?\n\nPress RETURN.";
		if (Input.GetKeyDown(KeyCode.Return)){
			exitOpen = !exitOpen;
			myState = States.controls;
		}
	}
	
	void washroom () {
		if (!sawWashroom) {textHolder = "You open the ocher moisty door and find that it is the access to the washroom. ";}
		else {
			textHolder = washroomThoughts[randomIndex];
			textHolder += "\n";
		}
		textHolder += "Close to the entrance there is a row of sinks. At the back you see an area " +
					"with showers and another one with toilet cabins. ";
		if (!hasTowel) {textHolder += "Over the sinks you spot a clean towel! Lucky! ";}
		textHolder += "\nWhere do you go? ";
		if (hasSmellyHand) {textHolder += "You could go wash your smelly hand in the sink. ";}
		else {textHolder += "You sure don't need to use the sinks to wash your hands. ";}
		if (!tookShower) {textHolder += "Perhaps try to take a relaxing shower? ";}
		else {textHolder += "The showers are no more a option... ";}
		if (!hasCoins) {textHolder += "Do you need to 'visit' the toilet? ";}
		else {textHolder += "You know there is nothing interesting anymore at the toilet section. ";} 
		if (!hasTowel) {textHolder += "You could of course grab the nice clean towel for yourself, before anyone comes by...";}
		textHolder += "\n\nPress ";
		if (hasSmellyHand) {textHolder += "K for the sinks";}
		if (hasSmellyHand & (!tookShower | !hasCoins | !hasTowel)) {textHolder += ", ";}
		if (!tookShower) {textHolder += "S for the showers";}
		if (!tookShower & (!hasCoins | !hasTowel)) {textHolder += ", ";}
		if (!hasCoins) {textHolder += "T for the toilets";}
		if (!hasCoins & !hasTowel) {textHolder += ", ";}
		if (!hasTowel) {textHolder += "L for the lucky towel";}
		if (hasSmellyHand | !tookShower | !hasCoins) {textHolder += " or ";}
		textHolder += "RETURN to go back to the courtyard.";
		
		if (Input.GetKeyDown(KeyCode.K) && hasSmellyHand){
			myState = States.sinks;
			sawWashroom = true;
			reset_randomIndex(washroomThoughts);
		} else if (Input.GetKeyDown(KeyCode.S) && !tookShower){
			myState = States.showers;
			sawWashroom = true;
			reset_randomIndex(washroomThoughts);
		} else if (Input.GetKeyDown(KeyCode.T) && !hasCoins){
			myState = States.toilets;
			sawWashroom = true;
			reset_randomIndex(washroomThoughts);
		} else if (Input.GetKeyDown(KeyCode.L) && !hasTowel){
			myState = States.towel;
			sawWashroom = true;
			reset_randomIndex(washroomThoughts);
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.courtyard;
			sawWashroom = true;
			reset_randomIndex(courtyardThoughts);
		}
	}
	
	void towel () {
		textHolder = "You grasp the towel as if there were competitors.\n\nPress RETURN to continue.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			hasTowel = true;
		}
	}
	
	void sinks () {
		textHolder = "The sinks are not particularly 'clean', but there is running water available. ";
		if (!hasSoap) {textHolder += "You also notice a loose tile in the wall near one of the sinks. ";}
		textHolder += "Do you wash your smelly hands? ";
		if (!hasSoap) {textHolder += "Or check the loose tile?";}
		textHolder += "\n\nPress W to wash your hands";
		if (!hasSoap) {textHolder += ", T to investigate the loose tile";}
		textHolder += " or RETURN to leave the sinks.";
		
		if (Input.GetKeyDown(KeyCode.W)){
			myState = States.washingHands;
			sawWashroom = true;
		} else if (Input.GetKeyDown(KeyCode.T) & !hasSoap){
			myState = States.tile;
			sawWashroom = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			sawWashroom = true;
		}
	}
	
	void washingHands () {
		if (hasSoap) {textHolder = "You wash your hands with the soap you found. You feel much better with clean and perfumed hands!";}
		else {textHolder = "You wash your hands. You feel much better with cleaner hands!";}
		textHolder += "\n\nPress RETURN to continue.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			hasSmellyHand = false;
		}
	}
	
	void tile () {
		textHolder = "You easily detach the tile. It hid a soap stash!\nYou grab some, just in case.\n\nPress RETURN to continue.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			hasSoap = true;
		}
	}
	
	void showers () {
		textHolder = "There is nothing at the shower area besides the showers themselves. ";
		if (hasTowel) {
			if (hasSoap) {textHolder += "You DO have soap and a towel... ";}
			else {textHolder += "Since you have at least a towel, you could try to take a shower. ";}
			textHolder += "Will you take a shower?";
		} else {
			if (hasSoap) {textHolder += "It is certainly convenient that you have found soap, but it would be nice to be able to dry yourself up " +
										"after a shower...";}
			else {textHolder += "The idea of taking a shower is tempting. However, there are some minimum requirements! Even in this situation. ";}
		}
		textHolder += "\n\nPress ";
		if (hasTowel) {textHolder += "S to take a shower or ";}
		textHolder += "RETURN to turn back.";
		
		if (Input.GetKeyDown(KeyCode.S) && hasTowel){
			myState = States.takingShower;
			tookShower = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
		}
	}
	
	void takingShower () {
		textHolder = "After undressing and putting away your belongings, You choose the least dirty shower and open the tap. " +
					"The water seems to be running with nice pressure and good temperature. ";
		if (hasSoap) {textHolder += "\nYou soap and wash your body. You feel relieved by noticing that all the dirt is dissolving into the drain.";}
		else {textHolder += "\nWhile washing your body you notice that the mixture of dirt and water is forming a sticky layer around your skin. " +
							"You feel even filthier than before!";}
		textHolder += "\nYou close the tap and start drying yourself up using the towel. Not a minute latter the pipes start making a horrible sound " +
					"and suddenly every shower starts shooting water all over the area. Fortunately, you escaped with your towel and dry clothes. " +
					"Taking another shower is not an option.\n\nPress RETURN to continue.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
		}
	}
	
	void toilets () {
		if (!sawToilets) {textHolder = "When approaching the toilets you find that all of them were already (profusely) used. Since you are not fond of toilet " +
										"cleaning, specially in post-apocalyptic situations, you decide that you can do your business somewhere else. " +
										"You observe though that one of the toilet cisterns is partially open. ";}
		else {
			textHolder = "The toilets stink horribly. ";
			if (!sawInsideCistern) {textHolder += "The cistern with the moved top picks your curiosity again...\nDo you want to see what is inside?";}
			else { if (!hasCoins) {textHolder += "There is the cistern with the strange object inside.\nDo you want to check it again?";}}
		}
		textHolder += "\n\nPress ";
		if (!hasCoins) {textHolder += "O to open the cistern or ";}
		textHolder += "RETURN to turn away from the toilets.";
		
		if (Input.GetKeyDown(KeyCode.O) && !hasCoins){
			myState = States.openCistern;
			sawToilets = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			sawToilets = true;
		}
	} 
	
	void openCistern () {
		if (!sawInsideCistern) {textHolder = "The cistern is full of water. Inside you see something, but you can't tell what it is. " +
										"You try to throw the cistern to empty it out, but it doesn't work.";}
		else {textHolder = "You see the strange shape dwelling at the bottom of the cistern...";}
		textHolder += "\nWill you try to grab it, even though you don't know what it is?\n\nPress T to try your luck or RETURN to leave the toilet area.";
		if (Input.GetKeyDown(KeyCode.T) && !hasCoins){
			myState = States.coins;
			sawInsideCistern = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			sawInsideCistern = true;
		}
	}
	
	void coins () {
		textHolder = "Tentatively you put your hand inside the cistern and get hold of some kind of small plastic bag filled with coins. " +
					"Well, well, well, you are a tresure hunter!\n\nPress RETURN to leave the toilet area with your bounty.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.washroom;
			hasCoins = true;
		}
	}
	
	void refectory () {
		if (!isSick) {
			if (!sawRefectory) {
				textHolder = "Passing the greenish door, you immediatly realize it leads to the refectory. " +
					"There are several tables bolted to the floor throughout the room and an area at the back for serving food. " +
						"There are only two other doors in the room: one next to the serving area, leading probably to the kitchen, and " +
						"another on the opposite corner, which has a sign that reads 'EXIT' " +
						"(Your are astonished by the irony of this prision's planning).\nYou notice that there is nothing over the tables " +
						"or in the serving area, though there is a menu poster hanging just a few steps from the entrance. " +
						"At one side of the room, next to the left wall there is a vending machine! Maybe there you will find something to eat!\n";} 
			else {
				textHolder = refectoryThoughts[randomIndex];
				textHolder += "\nWhat will it be?\n";
			}
			if (!sawExitClosed) {textHolder += "Are you going straight to the alleged exit? ";}
			else {
				if (!sawDog) {textHolder += "Are you going to have another try at the exit door? ";}
				else {textHolder += "Do you have a plan for overcoming the dog at the exit? ";}
			}
			textHolder += "Or do you stop by the menu poster? ";
			if (sawTables == 0) {textHolder += "Or verify if the tables have leftovers from mealtime? ";}
			else {textHolder += "Or check the tables again for any forgotten piece of food? ";}
			if (!sawKitchen) {textHolder += "Or perhaps it is wiser (under the circunstances) to just enter the kitchen and feed diretly from the source? ";}
			else {textHolder += "Or do you want to check the kitchen again? ";}
			if (foodBought.Count == 0) {textHolder += "You could also try to get something from the vending machine. ";}
			textHolder += "Or go back to the courtyard and continue exploring the rest of the prison." + 
				"\n\nPress E for the exit door, P for the poster, T for the tables, K for the kitchen, V for the vending machine, or RETURN to the courtyard.";
			
			if (Input.GetKeyDown(KeyCode.E)){
				myState = States.exitDoor;
				sawRefectory = true;
				reset_randomIndex(refectoryThoughts);
			} else if (Input.GetKeyDown(KeyCode.P)){
				myState = States.poster;
				sawRefectory = true;
				reset_randomIndex(refectoryThoughts);
			} else if (Input.GetKeyDown(KeyCode.T)){
				myState = States.tables;
				sawRefectory = true;
				reset_randomIndex(refectoryThoughts);
			} else if (Input.GetKeyDown(KeyCode.K)){
				myState = States.kitchen;
				sawRefectory = true;
				reset_randomIndex(refectoryThoughts);
			} else if (Input.GetKeyDown(KeyCode.V)){
				myState = States.vendingMachine;
				sawRefectory = true;
				reset_randomIndex(refectoryThoughts);
			} else if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.courtyard;
				sawRefectory = true;
				reset_randomIndex(courtyardThoughts);
			}
		}
		else {
			textHolder = "You start feeling dizzy and loose your balance at the middle of the refectory. " +
						"You fall in your knees and vomit violently for several minutes.\n\nOnce recovered, " +
						"you try to understand what happened...\n\nPress RETURN.";
			
			if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.refectory;
				wasSick = true;
				isSick = false;
			}
		}
		
	}
	
	void poster () {
		textHolder = "The poster is a plain dull black & white printout. It reads:\n" +
			"TODAY's LUNCH\n**************\nTuna salad\nMashed potatoes\nMeat balls\nYesterday's (written in grafitti) Rice\nPudding\n**************" +
			"\nQuite disappointing...\n\nPress RETURN.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
		}
	}
	
	void tables () {
		if (sawTables == 0) {textHolder = "You walk around the tables, confirming that there is no food around.";}
		else if (sawTables == 1) {textHolder = "You walk around the tables, confirming that there is REALLY no food around.";}
		else if (sawTables == 2) {textHolder = "You walk around the tables, confirming that there is STILL no food and probably never will.";}
		else if (sawTables == 3) {textHolder = "You walk around the tables, confirming that food did not sprout out of nothing since the last time.";}
		else {textHolder = "You walk around the tables, confirming that you must be either very hungry or an enthusiast of penitentiary furniture " +
							"since you are examining these tables for the " + sawTables + "th time.";}
		textHolder += "\n\nPress RETURN to explore somewhere else.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
			sawTables ++;
		}
	}
	
	void kitchen () {
		if (!sawKitchen) {textHolder = "You go through the door in the back and enter the kitchen. You see that everything is relatively in order, " +
										"no signs of mayhem. Unfortunately though, it seems completely cleansed of anything edible.\nThere is a " +
										"large metal bench for food preparation and behind it the door of an industrial cold room.";}
		else {
			textHolder = kitchenThoughts[randomIndex];
		}
		if (deadGuardOut) {textHolder += "\nThe corpse of the guard lies on the ground, exactly where you left it. That's a relief!";}
		if (hasKnife & hasTowerKey) {textHolder += "\nYou look around, but it seems there is nothing more to do here.";}
		else {textHolder += "\nWhat are you going to do? ";}
		if (!sawBench) {textHolder += "Inspect the bench? ";} else {if (!hasKnife) {textHolder += "Go back for the knife in the bench?";}}
		if (!sawColdRoom) {textHolder += "Look inside the cold room for any food left behind? ";}
		else if (!deadGuardOut & sawColdRoom) {textHolder += "Stare at the shape you saw in the cold room";}
		else if (deadGuardOut & !hasTowerKey) {textHolder += "Rummage the dead guard's body? ";}
		textHolder += "\n\nPress ";
		if (!hasKnife) {textHolder += "B for the bench"; if (!deadGuardOut | !hasTowerKey) {textHolder += ", ";} else {textHolder += " or ";}}
		if (!deadGuardOut) {textHolder += "C for the cold room or ";}
		else if (!hasTowerKey) {textHolder += "G for the guard's corpse or ";}
		textHolder += "RETURN to go back to the refectory.";
		
		if (Input.GetKeyDown(KeyCode.B)){
			myState = States.bench;
			sawKitchen = true;
			reset_randomIndex(kitchenThoughts);
		} else if (Input.GetKeyDown(KeyCode.C)){
			myState = States.coldRoom;
			sawKitchen = true;
			reset_randomIndex(kitchenThoughts);
		} else if (Input.GetKeyDown(KeyCode.G)){
			myState = States.deadGuard;
			sawKitchen = true;
			reset_randomIndex(kitchenThoughts);
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
			sawKitchen = true;
			reset_randomIndex(refectoryThoughts);
		}
	}
	
	void bench () {
		if (!sawBench) {textHolder = "Examining more closely the bench you see that the cooks left behind several equipment. " +
									"The single thing you think might be somehow useful is a medium size knife.";}
		else {textHolder = "Again, the only thing remotely useful here is a medium size knife.";}
		textHolder += "\n\nPress T to take it or RETURN to leave it.";
		
		if (Input.GetKeyDown(KeyCode.T)){
			myState = States.kitchen;
			hasKnife = true;
			sawBench = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.kitchen;
			sawBench = true;
		}
	}
	
	void coldRoom () {
		if (!sawColdRoom) {textHolder = "You open the cold room's door and almost die of shock! You see a human shape sitting at the back, " +
										" surrounded only by darkness and frost. You can't tell if it is dead or alive. ";}
		else {textHolder = "Carefully you open again the cold room's door and peak on the human shape inside. You see no movement... ";}
		textHolder += "Do you reach towards the figure and touch it? Could this person still be saved?\n\n" +
						"Press T to approach and touch it or RETURN to close the door and pretend you never saw it.";
		
		if (Input.GetKeyDown(KeyCode.T)){
			myState = States.deadGuard;
			sawColdRoom = true;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.kitchen;
			sawColdRoom = true;
		}
	}
	
	void deadGuard () {
		if (!deadGuardOut) {
			textHolder = "Wanting to look to the other side, you enter the cold room and reach out until you touch " +
						"something organic filled by ice crystals. You are touching a face!!!\nSince it is not moving, " +
						"you decide to quickly drag the figure out of the cold room by its shoulders.\nUnder the better light of the kitchen, " +
						"you can distinguish that it is a prison guard, completely and unequivocally dead.\n\nPress RETURN.";
			
			if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.deadGuard;
				deadGuardOut = true;
			}
		}
		else {
			textHolder = "The guard's body is defrosting quickly and it is starting to look even more scary than before. " +
						"You can now see that this guard was tied up and left, probably unconscious, inside the cold room. " +
						"Do you want to rummage into the guard's clothes or let this poor person's soul rest in peace?\n\nPress " +
						"R to plunder the dead or RETURN to be respectful.";
			
			if (Input.GetKeyDown(KeyCode.R)){
				myState = States.findTowerKey;
				hasTowerKey = true;
			} else if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.kitchen;
			}
		}
	}
	
	void findTowerKey () {
		textHolder = "You check every pocket of the guard's uniform and find a small security card key. " +
					"\n\nPress RETURN to take it.";
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.kitchen;
			hasTowerKey = true;
		}
		
	}
	
	void vendingMachine () {
		if (!sawVendingMachine) {textHolder = "You approach the vending machine. There are several products still in stock: " +
											"potato chips, gummy bears, apples, chocolate bars and bananas. " +
											"The machine is armoured (you ARE in a prison after all!). There is no way of getting products out, " +
											"except of course by buying them.";}
		else {
			if (!ate) {textHolder = "You gaze salivating at the all options of the vending machine.";}
			else if (!wasSick) {textHolder = "Although you are already full, you could get something for later.";}
			else {textHolder = "You still feel the burning of the last 'product' you got from this machine. " +
								"However, you cold get something else and save it for later.";}
		}
		if (!hasCoins) {textHolder += "\nBut you don't have any money!!!";}
		else {textHolder += "\nWell, you do dispose of a small fortune in coins. What will it be?";}
		textHolder += "\n\nPress ";
		if (hasCoins) {textHolder += "P to buy potato chips, G for a bag of gummy bears, A to get an apple, C for a chocolate bar, B for a banana or ";}
		textHolder += "RETURN to leave the machine.";
		
		if (hasCoins) {
			if (Input.GetKeyDown(KeyCode.P)){
				myState = States.food;
				foodBought.Add("potato chips");
				sawVendingMachine = true;
			} else if (Input.GetKeyDown(KeyCode.G)){
				myState = States.food;
				foodBought.Add("gummy bears");
				sawVendingMachine = true;
			} else if (Input.GetKeyDown(KeyCode.A)){
				myState = States.food;
				foodBought.Add("apple");
				sawVendingMachine = true;
			} else if (Input.GetKeyDown(KeyCode.C)){
				myState = States.food;
				foodBought.Add("chocolate bar");
				sawVendingMachine = true;
			} else if (Input.GetKeyDown(KeyCode.B)){
				myState = States.food;
				foodBought.Add("banana");
				sawVendingMachine = true;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
			sawVendingMachine = true;
		}
	}
	
	void food () {
		if (!ate) {textHolder = "Now that you have some food, what do you want to do with it?\n\nPress E to eat it or RETURN to save it for later.";}
		else {textHolder = "You save the " + foodBought[foodBought.Count - 1] + " for later.";}
		
		textHolder += "\n\nPress RETURN.";
		
		if (Input.GetKeyDown(KeyCode.E) & !ate){
			myState = States.sick;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
		}
		
	}
	
	void sick () {
		ate = true;
		textHolder = "You ate the " + foodBought[foodBought.Count - 1];
		if (foodBought[foodBought.Count - 1] == "apple" | foodBought[foodBought.Count - 1] == "banana") {
			isSick = true;
			textHolder += ", which tasted kind of funny...";
		} else {
			textHolder += ", which tasted as good as any other garbage food.";
		}
		textHolder += "\n\nPress RETURN.";
		if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.vendingMachine;
			string foodJustAte = foodBought[foodBought.Count - 1];
			foodBought.Remove(foodJustAte);
		}
	}
	
	void exitDoor () {
		if (!sawExitClosed & !exitOpen) {textHolder = "Happily, you push the exit door, just to find out that it is actually locked.";}
		else if (!sawExitClosed & exitOpen) {textHolder = "Happily, you push the exit door...\nand it IS open!!!";}
		else if (sawExitClosed & !exitOpen) {textHolder = "Hoping for a outcome different from the last time, you push the door...\nYeap, it is still closed.";}
		else if (sawExitClosed & exitOpen & !sawDog) {textHolder = "Hoping for a different outcome this time, you push the door...\nOH YEAH! It is open!!!";}
		else if (sawExitClosed & exitOpen & sawDog) {textHolder = "You open the exit door, carefully not to startle the dog...";}
		textHolder += "\n\nPress RETURN to continue.";
		if (Input.GetKeyDown(KeyCode.Return) & exitOpen){
			myState = States.dogCorridor;
			sawExitClosed = true;
		} else if (Input.GetKeyDown(KeyCode.Return) & !exitOpen){
			myState = States.refectory;
			sawExitClosed = true;
		}
	}
	
	void dogCorridor () {
		if (!sawDog) {textHolder = "You see a fierceful barking dog! You calm down realizing that it is on a leash and that it is several meters from you. " +
									"The room is a corridor about 10 meters long leading towards the outside of the prision. " +
									"You have a solid wall on your left and a grid wall on your right, where the dog's leash is tied to. " +
									"The dog is a german shepherd, probably a guard dog, and it seems to be abandoned here for several hours without food. " +
									"It will be definetly a challenge to get pass it.\n";}
		else {textHolder = "You enter the corridor and the dog starts barking rabidly at you.\n";}
		if (hasKnife | foodBought.Count != 0) {
			if (hasKnife) {textHolder += "You hold the knife and wonder if your only chance of survival is to kill this forsaken creature. ";}
			if (hasKnife & foodBought.Count != 0) {textHolder += "However, you do think of another, more gentle, solution: " + foodBought[foodBought.Count - 1] + "! ";}
			else if (!hasKnife & foodBought.Count != 0) {textHolder += "Considering your situation, it could be wise to share your food with it. ";}
			textHolder += "\nAs a last resort, you can always ";
		}
		else {textHolder += "\nYou can't do anything but to ";}
		textHolder += "make an attempt to pass through the dog.\n\nPress ";
		if (hasKnife) {textHolder += "A to attack the dog with the knife you found in the kitchen, ";}
		if (foodBought.Count != 0) {textHolder += "F to try to feed the " + foodBought[foodBought.Count - 1] + " to the dog, ";}
		textHolder += "P to try to pass through avoiding the dog, or RETURN to go retreat to the refectory.";
		
		if (Input.GetKeyDown(KeyCode.A) & hasKnife){
			myState = States.dogAssault;
		} else if (Input.GetKeyDown(KeyCode.F) & foodBought.Count != 0){
			myState = States.dogFeed;
		} else if (Input.GetKeyDown(KeyCode.P)){
			myState = States.passingTheDog;
		} else if (Input.GetKeyDown(KeyCode.Return)){
			myState = States.refectory;
			sawDog = true;
		}
	}
	
	void passingTheDog () {
		
		if (theEnd) {
			if (outcome) {textHolder = "You run as fast as you can through the left side. The dog attack you, but you jump away just in time. " +
				"You did it!!!\nYou are now at the other side of the corridor. You continue until the end of the cooridor, reaching the outside. " +
				"The gate is open and there are no signs of life out here. What happened?";}
			else {textHolder = "You run fast through the right side, trying to jump the dog's leash. However, your feet hook on the streached leash " +
								"and you fall down miserably! The dog does not hesitate in biting your throat rabidly. You try to fight, but you are already " +
								"feeling too dizzy... Everything fades away...";}
			textHolder += "\n\nTHE END\nPress RETURN to play again.";
			if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.start;
			}
		} else {
			textHolder = "You move slow and quietly against the wall, keeping the the greatest possible distance between the dog and you. " +
				"However, when approaching the dog's position you see that it is not enough, specially because the dog is constantly " +
					"streaching the leash in your direction.\nYou then decide to run through an opening between the dog and the wall.\n";
			textHolder += "\nPress SPACE.";
			if (Input.GetKeyDown(KeyCode.Space)){ outcome = Random.value < 0.33f; print(outcome); theEnd = true; }
		}
	}
	
	void dogAssault () {
		
		if (theEnd){
			if (outcome) {textHolder = "In a strike of good luck, the dog jumps at you just before you are actually within the leash reach. " +
										"With the dog briefly contorted for stopping short, you seize the opportunity and thurst the animal throat. " +
										"It dies within seconds. You are both accelerated and sad about the violence you just performed. " +
										"Will your life beyond this prison be heartless as this moment?";}
			else {textHolder = "The dog stares at you with fierceful eyes. When you find yourself close enough, you strike with a sideswipe, " +
								"missing the dog completely. Although it is weak from famine, the dog proves to be quite agile. Once your clumsy " +
								"attack was diverted, the dog jumps at you biting your throat. Instinctively you stab the dog between the ribs and get it to release you. " +
								"Unfortunately, both of you are deadly wounded and after a few seconds contemplating the dog dying out everything fades away...";}
			textHolder += "\n\nTHE END\nPress RETURN to play again.";
			if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.start;
			}
		} else {
			textHolder = "You prepare yourself and steadly advance with your knife towards the dog.\n";
			textHolder += "\nPress SPACE.";
			if (Input.GetKeyDown(KeyCode.Space)){ outcome = Random.value < 0.66f; theEnd = true; }
		}
	}
	
	void dogFeed () {
		
		if (theEnd) {
			if (foodBought[foodBought.Count - 1] == "banana" | foodBought[foodBought.Count - 1] == "apple") {
				textHolder = "Halfway through the corridor, the dog stops, wails, vomits the food you just gave it and falls down ill. ";
				if (wasSick) {textHolder += "You should have known that this kind of food was poisonous since you just had a crisis yourself. " +
					"Although you could recover, the dog is apperently too weak and it is already breathing hard. ";}
				else {textHolder += "You don't understand what is happening. Was it the food? Whatever it was, you can tell that the dog will not recover from it. ";}
				if (hasKnife) {textHolder += "Full of sadness, you decide to stop the dog's suffering with your knife. You do it as quickly as possible. ";}
				else {textHolder += "You don't really know what to do. After minutes of doubt looking at the dying animal, you just continue your way with tears " +
					"falling out your eyes. ";}
				textHolder += "You must continue your path alone again...";
			} else {textHolder = "Doggy and you reach the outside of the prison. The gate is open and there are no signs of life out here. What happened???";}
			textHolder += "\n\nTHE END\n\nPress RETURN to play again.";
			if (Input.GetKeyDown(KeyCode.Return)){
				myState = States.start;
			}
		} else {
			textHolder = "Avoiding getting too close and speaking 'heeere, doooggy' with a kind voice, you smoothly throw the " + foodBought[foodBought.Count - 1] + " next to the dog. " +
					"After a brief suspicious sniffing, the dog quickly eats the whole thing up. The dog then looks at you with needy eyes, obviously waiting for more. " +
					"Well, at least it seems much more 'pacified'. You think you can now approach it. Not only the dog doesn't attack you, but it actually starts shaking the " +
					"tail and licking your hands. ";
			if (hasSmellyHand) {textHolder += "Ow, now you realize that you should have washed your smelly hands... ";}
			textHolder += "You decide to set free the dog by removing its leash. It seems even more grateful.\nYou then direct yourself to the end of the corridor " +
						"in your way to freedom and followed by your new friend!\n";
			textHolder += "\nPress SPACE.";
			if (Input.GetKeyDown(KeyCode.Space)){ theEnd = true; }
		}
	}
}
