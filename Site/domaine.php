<!DOCTYPE HTML>
<html>
	<head>
		<title>ePic - Project</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<link rel="stylesheet" href="style.css" />
    <link rel="icon" href="favicon.ico"/>
	</head>
	<body class="subpage">

    <!-- Header -->
			<header id="header">
				<div class="inner">
          <a href="http://www.utc.fr/si28/" class="logo">SI28</a>
						<a href="index.html">Plateforme</a>
            <a href="mission.html">Mission</a>
            <a href="squad.html">Escouade</a>
				</div>
			</header>
<?php
  switch($_POST['query']) {
    case "ᙜᗎᗧᘙᗋ":
      echo "<section id='main' class='wrapper'>
        <div class='inner'>
          <header class='align-center'>
            <img src='logo.png' title='UTC Cosmique' alt='Logo UTC' width='100' height='100'/>
            <h2>Accès autorisé au Domaine</h2>
            <p>Youpiii</p>
          </header>
          <center><a href='teleportation.html' class='button special'>Accéder aux données</a></center>";
      break;
		case "我是法国人":
      echo "<section id='main' class='wrapper'>
        <div class='inner'>
          <header class='align-center'>
            <img src='logo.png' title='UTC Cosmique' alt='Logo UTC' width='100' height='100'/>
            <h2>Accès autorisé au Domaine</h2>
            <p>Youpiii</p>
          </header>
          <center><a href='pierre.html' class='button special'>Accéder aux données</a></center>";
      break;
		case "我喜欢啤酒":
      echo "<section id='main' class='wrapper'>
        <div class='inner'>
          <header class='align-center'>
            <img src='logo.png' title='UTC Cosmique' alt='Logo UTC' width='100' height='100'/>
            <h2>Accès autorisé au Domaine</h2>
            <p>Youpiii</p>
          </header>
          <center><a href='motivation.html' class='button special'>Accéder aux données</a></center>";
      break;
    default:
      echo "<section id='main' class='wrapper'>
        <div class='inner'>
          <header class='align-center'>
            <img src='logo.png' title='UTC Cosmique' alt='Logo UTC' width='100' height='100'/>
            <h2>Accès refusé au Domaine</h2>
            <p>Mdair le boloss</p>
          </header>
          <center><a href='mission.html' class='button special'>Retour au terminal</a></center>";
  }
 ?>

</div>
</section>

 <!-- Footer -->
   <footer id="footer">
     <div class="inner">
       <div class="flex">
         <div class="copyright">
           &copy; ePic - Project.
         </div>
         <ul class="icons">
           <li><a href="https://www.facebook.com/groups/161815034547928/"><span class="label"><img src="facebook.jpg" alt="Facebook" title="Like salo" width="50" height="50"/></span></a></li>
           <li><a href="https://www.utc.fr/"><img src="logoutc.png" alt="Logo UTC" title="UTC de Compiègne" width="150" height="50"/></a></li>
         </ul>
       </div>
     </div>
   </footer>
