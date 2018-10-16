﻿$(document).ready(function()
{
  var randomquote;
  var randomsource;
  var randomcolor;
  var randomnum;
  
  quoting();
  // 페이지 로딩할때 함수 자동 실행된다 (quoting)
  
  // 생략하면  랜덤버튼 누르고 나서 실행되기에, 
  //=트윗버튼은 랜덤버튼을 한번 눌러줘야 활성화! 된다....
  
var x = document.getElementById("rbt")
x.addEventListener("click",quoting,false);  
// quoting 실행 ("클릭", quoting 함수실행)
// javascript (수업시간에 배움)

// $(".rightbt").on("click",function(){quoting()});
// jquery 로 하는 방법 (유투브에서 배움)
  
var y = document.getElementById("lbt")
y.addEventListener("click",tweet,false);  
// tweet 실행 ("클릭", tweet 함수실행) 
  
  function tweet()
{
  
 window.open("http://www.twitter.com/intent/tweet?text="
             +randomquote+ " ▶ " +randomsource); 
}
// 트윗출력함수
  
  function quoting()
  { 
var quotes = ["Every year as Easter approaches, stores are filled with jelly beans, candy eggs, egg-coloring kits, bunnies of all types, and baskets for c­arrying all of this bounty. However, most of us know that Easter isn't simply a ­commercial festival about dyeing and hiding eggs or wearing new spring attire.",
"Easter is the Christian observance of the crucifixion of Jesus Christ and his resurrection days later. It's the central festival of the Christian church and, after the Sabbath, it's the oldest Christian observance. ",
"Unlike festivals such as Christmas, Easter has been celebrated without interruption since New Testament times",
"If you l­ive outside the UK, you probably haven't heard of Shrove Tuesday. But you probably know it by its other name, Mardi Gras. Pancakes were originally eaten o­n Shrove Tuesday -- the Tuesday before Lent -- to use up eggs and fat before the fast of Lent. Today, these pancakes are generally made of eggs, milk and flour. The word <shrove> comes from <shrive> meaning  <the confessions of sins> -- something done in preparation for Lent.",
"Ash Wed­nesday is a day of fasting that gets its name from the practice of sprinkling ashes over those engaging in the fast of Lent. Has anyone ever apologized to you by saying, <Let me put on my ashes and sackcloth...> This is where that saying originated."];
    
var sources = [
"How Easter Works",
"How Easter Works",
"How Easter Works",
"Shrove Tuesday",
"Ash Wednesday"];
    
var colors = ["#eef6f1","#f6fbff","#fffcfa","#f6ffea","#f9f6f9"];
    
randomnum = Math.floor((Math.random()*quotes.length));
    
randomquote = quotes[randomnum];
randomsource = sources[randomnum];
randomcolor=  colors[randomnum];
   
var aa = document.getElementById("qt");
aa.innerHTML= randomquote;
aa.style.backgroundColor =randomcolor;  
    
var bb = document.getElementById("sc");
bb.innerHTML= randomsource;


// javascript 수업시간에 배움 (getElementById)

//$(".quote").text(randomquote);
//$(".source").text(randomsource);
// 유툽으로 배움 (class 이름 써서 jquery)      
 }  
 // quote 출력함수
});

