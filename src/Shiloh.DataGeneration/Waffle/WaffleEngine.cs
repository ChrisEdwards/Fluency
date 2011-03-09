/*
 * Title: The Waffle Generator
 * Author: Andrew Clarke
 * 
 * The article about this generator and how it was created can be found at:
 * http://www.simple-talk.com/dotnet/.net-tools/the-waffle-generator/
 */

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Shiloh.Utils;


namespace SimpleTalk.WaffleGenerator
{
	public class WaffleEngine
	{
		static readonly string[] s_PreamblePhrases = IO.ForEachLineIn( "PreamblePhrases.txt" ).ToArray();
			/* new[]
		                                             	{
		                                             			"In broad terms, we can define the main issues with |t. There are :-<DL><DT>The |o of |o<DD>|B |C |D.<DT>The |o of |o<DD>|B |C |D.<DT>The |o of |o<DD>|B |C |D.<DT>The |o of |o<DD>|B |C |D.</DL><p>"
		                                             			,
		                                             			"The following points should be appreciated about |t; <ol><li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.</ol>|n    "
		                                             			,
		                                             			"Note that:- (1)|B |C |D..(2)|B |C |D..(3)|B |C |D.(4)|B |C |D.(5)|B |C |D.(6)|B |C |D.|n  ",
		                                             			"Essentially; <ul><li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.</ul>|n   ",
		                                             			"To make the main points more explicit, it is fair to say that; <ul><li>|B |C |D.<li>|B |C |D.<li>|B |C |D.<li>|B |C |D.</ul>|n   "
		                                             			,
		                                             			"We have heard it said, tongue-in-cheek, that",
		                                             			"To be quite frank,",
		                                             			"Focussing on the agreed facts, we can say that",
		                                             			"To be perfectly truthful,",
		                                             			"In broad terms,",
		                                             			"To be perfectly honest,",
		                                             			"It was |f |s who first pointed out that",
		                                             			"Since |f |s's first formulation of the |c, it has become fairly obvious that",
		                                             			"Since the seminal work of |f |s it has generally been accepted that",
		                                             			"Without a doubt, |f |s iwas right in saying that",
		                                             			"As regards |h |c, We should put this one to bed. On the other hand,",
		                                             			"As regards |h |c, This may have a knock-on effect. On the other hand,",
		                                             			"We must take on board that fact that",
		                                             			"Without a doubt, |B |C |D. So, where to from here? Persumably,",
		                                             			"It has hitherto been accepted that",
		                                             			"At the end of the day,",
		                                             			"Under the provision of the overall |1 plan,",
		                                             			"Firming up the gaps, one can say that",
		                                             			"Within the bounds of |h |c,",
		                                             			"The |h |c provides us with a win-win situation. Especially if one considers that ",
		                                             			"There are swings and roundabouts in considering that",
		                                             			"To be precise,",
		                                             			"Whilst taking the subject of  |h |c offline, one must add that",
		                                             			"For example,",
		                                             			"An orthodox view is that",
		                                             			"To reiterate,",
		                                             			"To recapitulate,",
		                                             			"Strictly speaking,",
		                                             			"In a very real sense,",
		                                             			"Regarding the nature of |h |c,",
		                                             			"In a strictly mechanistic sense,",
		                                             			"One is struck quite forcibly by the fact that",
		                                             			"In any event,",
		                                             			"In particular,",
		                                             			"In assessing the |c, one should think outside the box. on the other hand,",
		                                             			"On the other hand,",
		                                             			"It is recognized that",
		                                             			"Focusing specifically on the relationship between |h |c and any |c,",
		                                             			"Although it is fair to say that |B |C |D, one should take this out of the loop",
		                                             			"|bly,",
		                                             			"|bly,",
		                                             			"|bly,",
		                                             			"Be that as it may,",
		                                             			"Taking everything into consideration,",
		                                             			"As in so many cases, we can state that",
		                                             			"The |c cannot explain all the problems in maximizing the efficacy of |h |c. Generally",
		                                             			"We can confidently base our case on an assumption that",
		                                             			"An initial appraisal makes it evident that",
		                                             			"An investigation of the |1 factors suggests that",
		                                             			"It is common knowledge that",
		                                             			"Despite an element of volatility,",
		                                             			"The less obviously co-existential factors imply that",
		                                             			"To coin a phrase,",
		                                             			"One might venture to suggest that",
		                                             			"In all foreseeable circumstances,",
		                                             			"However,",
		                                             			"Similarly,",
		                                             			"As a resultant implication,",
		                                             			"There is a strong body of opinion that affirms that",
		                                             			"Up to a point,",
		                                             			"Quite frankly,",
		                                             			"In this regard,",
		                                             			"Based on integral subsystems,",
		                                             			"For example,",
		                                             			"Therefore,",
		                                             			"Within current constraints on manpower resources,",
		                                             			"Up to a certain point,",
		                                             			"In an ideal environment,",
		                                             			"It might seem reasonable to think of |h |c as involving |h |c. Nevertheless,",
		                                             			"It can be forcibly emphasized that",
		                                             			"Thus,",
		                                             			"Within the restrictions of |h |c,",
		                                             			"In respect to specific goals,",
		                                             			"It is important to realize that",
		                                             			"To put it concisely,",
		                                             			"To be perfectly frank,",
		                                             			"On any rational basis,",
		                                             			"In any event,",
		                                             			"On the basis of |h |2 |3,",
		                                             			"With all the relevant considerations taken into account, it can be stated that",
		                                             			"Few would disagree, however, that",
		                                             			"It goes without saying that",
		                                             			"Only in the case of the |c can one state that",
		                                             			"if one considers the |c in the light of |h |c,",
		                                             			"The |c is taken to be a |c. Presumably,",
		                                             			"So far,",
		                                             			"It is quite instructive to compare |h |c and |h |c. In the latter case,",
		                                             			"Obviously,",
		                                             			"By and large,",
		                                             			"Possibly,",
		                                             			"One can, with a certain degree of confidence, conclude that",
		                                             			"Without doubt,",
		                                             			"With due caution, one can postulate that",
		                                             			"The |c is clearly related to |h |c. Nevertheless,",
		                                             			"There is probably no causal link between the |c and |h |c. However",
		                                             			"In the light of |h |c, it is clear that",
		                                             			"No one can deny the relevance of |h |c. Equally it is certain that",
		                                             			"Albeit,",
		                                             			"It is precisely the influence of |h |c for |t that makes the |c inevitable, Equally,",
		                                             			"One must clearly state that",
		                                             			"In connection with |h |c,",
		                                             			"Normally",
		                                             			"one can, quite consistently, say that",
		                                             			"Clearly, it is becoming possible to resolve the difficulties in assuming that",
		                                             			"Within normal variability,",
		                                             			"There can be little doubt that",
		                                             			"Few would deny that",
		                                             			"It is not often |e stated that",
		                                             			"In real terms,",
		                                             			"In this day and age,",
		                                             			"It is |e stated that",
		                                             			"The position in regard to the |c is that",
		                                             			"On one hand |B |C |D, but on the other hand",
		                                             			"One hears it stated that |B |C |D, but it is more likely that",
		                                             			"Whilst it may be true that |B |C |D, one must not lose sight of the fact that"
		                                             	};
			 * */

		static readonly string[] s_SubjectPhrases = IO.ForEachLineIn("SubjectPhrases.txt").ToArray();
			/*new[]
		                                            	{
		                                            			"|h strategic goals",
		                                            			"|h gap analysis",
		                                            			"|h hardball",
		                                            			"|h purchaser - provider",
		                                            			"|h skill set",
		                                            			"|h knock-on effect",
		                                            			"|h strategic plan ",
		                                            			"|h the bottom line",
		                                            			"|h mindset",
		                                            			"|h benchmark",
		                                            			"|h core business",
		                                            			"|h  big picture",
		                                            			"|h take home message",
		                                            			"|h lessons learnt",
		                                            			"|h movers and shakers",
		                                            			"|h knowledge base",
		                                            			"the ball-park figures for the |c",
		                                            			"The core drivers",
		                                            			"a particular factor, such as the |c, the |c, the |c or the |c",
		                                            			"there is an apparent contradiction between the |c and |h |c. However, |h |c",
		                                            			"the question of |h |c",
		                                            			"the desirability of attaining |h |c, as far as the |c is concerned,",
		                                            			"a persistent instability in |h |c",
		                                            			"examination of |2 instances",
		                                            			"the classic definition of |h |c",
		                                            			"firm assumptions about |c",
		                                            			"the |c and the resources needed to support it are mandatory. |A |B",
		                                            			"significant progress has been made in the |c. |A |B",
		                                            			"efforts are already underway in the development of the |c. |A |B",
		                                            			"a |2 operation of |h |c",
		                                            			"subdivisions of |h |c",
		                                            			"an anticipation of the effects of any |c",
		                                            			"an overall understanding of |h |c",
		                                            			"the assertion of the importance of the |c",
		                                            			"an understanding of the necessary relationship between the |c and any |c",
		                                            			"the possibility, that the |c plays a decisive part in influencing |h |c, ",
		                                            			"any solution to the problem of |h |c",
		                                            			"the lack of understanding of |h |c",
		                                            			"the |c in its relation to |h |c",
		                                            			"parameters within |h |c",
		                                            			"the target population for |h |c",
		                                            			"initiation of |h |c",
		                                            			"both |c and |c",
		                                            			"|h |c",
		                                            			"an extrapolation of the |c",
		                                            			"|h |c",
		                                            			"the assessment of any significant weaknesses in the |c",
		                                            			"any subsequent interpolation",
		                                            			"|h |c is |e significant. On the other hand |h |c",
		                                            			"|h |c relates |e to any |c. Conversely, |h |c",
		                                            			"|h |c may be |e important. The |c",
		                                            			"the incorporation of the |c",
		                                            			"the quest for the |c",
		                                            			"the dangers inherent in the |c",
		                                            			"the value of the |c",
		                                            			"the |c",
		                                            			"an unambiguous concept of the |c",
		                                            			"a metonymic reconstruction of the |c",
		                                            			"a primary interrelationship between system and/or subsystem  technologies"
		                                            	};
			 */

		static readonly string[] s_VerbPhrases = IO.ForEachLineIn("VerbPhrases.txt").ToArray();
			/*new[]
		                                         	{
		                                         			"|d the overall efficiency of",
		                                         			"|d the |4 and |C",
		                                         			"can fully utilize",
		                                         			"will move the goal posts for",
		                                         			"would stretch the envelope of",
		                                         			"enables us to tick the boxes of",
		                                         			"could go the extra mile for",
		                                         			"should empower employees to produce",
		                                         			"should touch base with",
		                                         			"probably |d",
		                                         			"is generally compatible with",
		                                         			"provides the bandwidth for",
		                                         			"gives a win-win situation for",
		                                         			"has clear ramifications for",
		                                         			"has been made imperative in view of",
		                                         			"provides the context for",
		                                         			"underpins the importance of",
		                                         			"focuses our attention on",
		                                         			"will require a substantial amount of effort. |A |B |C",
		                                         			"represents a different business risk.  |A |B |C",
		                                         			"is of considerable importance from the production aspect. |A |B |C",
		                                         			"should facilitate information exchange.  |A |B |C",
		                                         			"has the intrinsic benefit of resilience, unlike the",
		                                         			"cannot be shown to be relevant. This is in contrast to",
		                                         			"cannot always help us.  |A |B |C",
		                                         			"|C |D. A priority should be established based on a combination of |c and |c",
		                                         			"|C |D. The objective of the |c is to delineate",
		                                         			"shows an interesting ambivalence with",
		                                         			"underlines the essential paradigm of",
		                                         			"can be taken in juxtaposition with",
		                                         			"provides an interesting insight into",
		                                         			"must seem oversimplistic in the light of",
		                                         			"seems to |e reinforce the importance of",
		                                         			"leads clearly to the rejection of the supremacy of",
		                                         			"allows us to see the clear significance of",
		                                         			"underlines the significance of",
		                                         			"reinforces the weaknesses in",
		                                         			"confuses the |c and",
		                                         			"|d the |c and",
		                                         			"|d", "|d", "|d", "|d", "|d", "|d",
		                                         			"provides a harmonic integration with",
		                                         			"is constantly directing the course of",
		                                         			"must intrinsically determine",
		                                         			"has fundamental repercussions for",
		                                         			"provides an idealized framework for",
		                                         			"|e alters the importance of",
		                                         			"|e changes the interrelationship between the|c and",
		                                         			"|e legitimises the significance of",
		                                         			"must utilize and be functionally interwoven with",
		                                         			"|d the probability of project success and",
		                                         			"|e |d the |c and",
		                                         			"|e |d the |c and",
		                                         			"|e |d the |c and",
		                                         			"|e |d the |c in its relationship with",
		                                         			"|d the dangers quite |e of",
		                                         			"has confirmed an expressed desire for",
		                                         			"is reciprocated by",
		                                         			"has no other function than to provide",
		                                         			"adds explicit performance limits to",
		                                         			"must be considered proactively, rather than reactively, in the light of",
		                                         			"necessitates that urgent consideration be applied to",
		                                         			"requires considerable systems analysis and trade-off studies to arrive at",
		                                         			"provides a heterogenous environment to",
		                                         			"cannot compare in its potential exigencies with",
		                                         			"is further compounded, when taking into account",
		                                         			"presents extremely interesting challenges to",
		                                         			"|d the importance of other systems and the necessity for",
		                                         			"provides one of the dominant factors of",
		                                         			"forms the basis for",
		                                         			"enhances the efficiency of",
		                                         			"develops a vision to leverage",
		                                         			"produces diagnostic feedback to",
		                                         			"capitalises on the strengths of",
		                                         			"effects a significant implementation of",
		                                         			"seems to counterpoint",
		                                         			"adds overriding performance constraints to",
		                                         			"manages to subsume",
		                                         			"provides a balanced perspective to",
		                                         			"rivals, in terms of resource implications,",
		                                         			"contrives through the medium of the |c to emphasize",
		                                         			"can be developed in parallel with",
		                                         			"commits resources to",
		                                         			"confounds the essential conformity of",
		                                         			"provides the bridge between the |c and",
		                                         			"should be provided to expedite investigation into",
		                                         			"poses problems and challenges for both the |c and",
		                                         			"should not divert attention from",
		                                         			"provides an insight into",
		                                         			"has considerable manpower implications when considered in the light of",
		                                         			"may mean a wide diffusion of the |c into",
		                                         			"makes little difference to",
		                                         			"focuses our attention on",
		                                         			"exceeds the functionality of",
		                                         			"recognizes deficiencies in ",
		                                         			"needs to be factored into the equation alongside the",
		                                         			"needs to be addessed along with the"
		                                         	};
			 */

		static readonly string[] s_ObjectPhrases = new[]
		                                           	{
		                                           			"the overall game-plan",
		                                           			"the slippery slope",
		                                           			"the strategic fit",
		                                           			"The total quality objectives",
		                                           			"the |c. This should be considered in the light of the |c",
		                                           			"the |c. One must therefore dedicate resources to the |c immediately.",
		                                           			"the |c on a strictly limited basis",
		                                           			"this |c. This should present few practical problems",
		                                           			"what should be termed the |c",
		                                           			"the applicability and value of the |c",
		                                           			"the |c or the |c",
		                                           			"the negative aspects of any |c",
		                                           			"an unambiguous concept of the |c",
		                                           			"the thematic reconstruction of |c",
		                                           			"the scientific |o of the |c",
		                                           			"the evolution of |2 |o over a given time limit",
		                                           			"any commonality between the |c and the |c",
		                                           			"the greater |c of the |c",
		                                           			"the universe of |o",
		                                           			"any discrete or |2 configuration mode",
		                                           			"the |4",
		                                           			"an elemental change in the |c",
		                                           			"the work being done at the 'coal-face'",
		                                           			"what is beginning to be termed the \"|c\"",
		                                           			"the |c. We need to be able to rationalize |D",
		                                           			"the |c. We can then |e play back our understanding of |D",
		                                           			"the |c. Everything should be done to expedite |D",
		                                           			"The |c. The advent of the |c |e |d |D",
		                                           			"the |c. The |c makes this |e inevitable",
		                                           			"the |c. The |3 is of a |2 nature",
		                                           			"the |c. This may be due to a lack of a |c.",
		                                           			"the |c. Therefore a maximum of flexibility is required",
		                                           			"any |c. This can be deduced from the |c",
		                                           			"the |c. This may |e flounder on the |c",
		                                           			"the |c. This may explain why the |c |e |d |D",
		                                           			"the |c. This trend may dissipate due to the |c"
		                                           	};

		static readonly string[] s_Adverbs = new[]
		                                     	{
		                                     			"substantively", "intuitively", "uniquely", "semantically", "necessarily",
		                                     			"stringently", "precisely", "rigorously", "broadly", "generally",
		                                     			"implicitly", "inherently", "presumably", "preeminently",
		                                     			"analytically", "logically", "ontologically", "wholly", "basically",
		                                     			"demonstrably", "strictly", "functionally", "radically", "definitely",
		                                     			"positively", "intrinsically", "generally", "overwhelmingly",
		                                     			"essentially", "vitally", "operably", "fundamentally", "significantly",
		                                     			"retroactively", "retrospectively", "globally", "clearly", "disconcertingly"
		                                     	};

		static readonly string[] s_Verbs = new[]
		                                   	{
		                                   			"stimulates", "spreads", "improves", "energises", "emphasizes", "subordinates",
		                                   			"posits", "perceives", "de-stabilizes", "Revisits",
		                                   			"connotes", "signifies", "indicates", "increases", "supports", "rationalises",
		                                   			"provokes", "de-actualises", "relocates", "yields", "implies",
		                                   			"designates", "reflects", "sustains", "supplements", "represents",
		                                   			"re-iterates", "juxtasposes", "provides", "maximizes", "identifies",
		                                   			"furnishes", "supplies", "affords", "yields", "formulates", "focuses on",
		                                   			"depicts", "embodies", "exemplifies", "expresses", "personifies", "symbolizes", "typifies",
		                                   			"replaces", "supplants", "denotes", "depicts", "expresses", "illustrates", "implies",
		                                   			"symbolizes", "delineates", "depicts", "illustrates", "portrays", "clarifies", "depicts",
		                                   			"interprets", "delineates", "reflects", "evinces", "expresses", "indicates",
		                                   			"manifests", "reveals", "shows", "delineates", "represents", "anticipates", "denotes",
		                                   			"identifies", "indicates", "symbolizes", "diminishes", "lessens", " represses",
		                                   			"suppresses", "weakens", "accentuates", "amplifies", "heightens", "highlights",
		                                   			"spotlights", "stresses", "underlines", "underscores", "asserts", "reiterates",
		                                   			"restates", "stresses", "enhances", "amends", "translates", "specifies"
		                                   	};

		static readonly string[] s_FirstAdjectivePhrases = new[]
		                                                   	{
		                                                   			"comprehensive", "targeted", "realigned", "client focussed", "best practice", "value added", "quality driven",
		                                                   			"basic", "principal", "central", "essential", "primary", "indicative", "continuous",
		                                                   			"critical", "prevalent", "preeminent", "unequivocal", "sanctioned", "logical",
		                                                   			"reproducible", "methodological", "relative", "integrated", "fundamental",
		                                                   			"cohesive", "interactive", "comprehensive", "critical", "potential", "vibrant",
		                                                   			"total", "additional", "secondary", "primary", "heuristic", "complex", "pivotal",
		                                                   			"quasi-effectual", "dominant", "characteristic", "ideal", "doctrine of the", "key",
		                                                   			"independent", "deterministic", "assumptions about the", "heuristic", "crucial",
		                                                   			"meaningful", "implicit", "analogous", "explicit", "integrational", "non-viable",
		                                                   			"directive", "consultative", "collaborative", "delegative", "tentative",
		                                                   			"privileged", "common", "hypothetical", "metathetical", "marginalised", "systematised",
		                                                   			"evolutional", "parallel", "functional", "responsive", "optical", "inductive",
		                                                   			"objective", "synchronised", "compatible", "prominent", "three-phase", "two-phase",
		                                                   			"balanced", "legitimate", "subordinated", "complementary", "proactive",
		                                                   			"truly global", "interdisciplinary", "homogeneous", "hierarchical", "technical",
		                                                   			"alternative", "strategic", "environmental", "closely monitored", "three-tier",
		                                                   			"inductive", "fully integrated", "fully interactive", "ad-hoc", "ongoing", "proactive",
		                                                   			"dynamic", "flexible", "verifiable", "falsifiable", "transitional",
		                                                   			"mechanism-independent", "synergistic", "high-level"
		                                                   	};

		static readonly string[] s_SecondAdjectivePhrases = new[]
		                                                    	{
		                                                    			"fast-track", "transparent", "results-driven",
		                                                    			"subsystem", "test", "configuration", "mission", "functional", "referential",
		                                                    			"numinous", "paralyptic", "radical", "paratheoretical", "consistent", "macro",
		                                                    			"interpersonal", "auxiliary", "empirical", "theoretical", "corroborated",
		                                                    			"management", "organizational", "monitored", "consensus", "reciprocal",
		                                                    			"unprejudiced", "digital", "logic", "transitional", "incremental", "equivalent", "universal",
		                                                    			"sub-logical", "hypothetical", "conjectural", "conceptual", "empirical",
		                                                    			"spatio-temporal", "third-generation", "epistemological", "diffusible", "specific",
		                                                    			"non-referent", "overriding", "politico-strategical", "economico-social", "on-going",
		                                                    			"extrinsic", "intrinsic", "multi-media", "integrated", "effective", "overall",
		                                                    			"principal", "prime", "major", "empirical", "definitive", "explicit", "determinant",
		                                                    			"precise", "cardinal",
		                                                    			"principal", "affirming", "harmonizing", "central", "essential", "primary", "indicative",
		                                                    			"mechanistic", "continuous", "critical", "prevalent", "preeminent", "unequivocal", "sanctioned",
		                                                    			"logical ", "reproducible", "methodological", "relative", "integrated", "fundamental", "cohesive",
		                                                    			"interactive", "comprehensive", "critical", "potential", "total", "additional", "secondary",
		                                                    			"primary", "heuristic", "complex", "pivotal", "quasi-effectual", "dominant", "characteristic",
		                                                    			"ideal", "independent", "deterministic", "heuristic", "crucial", "meaningful", "implicit",
		                                                    			"analogous", "explicit", "integrational", "directive", "collaborative", "entative", "privileged",
		                                                    			"common", "hypothetical", "metathetical", "marginalised", "systematised", "evolutional",
		                                                    			"parallel", "functional", "responsive", "optical", "inductive", "objective", "synchronised",
		                                                    			"compatible", "prominent", "legitimate", "subordinated ", "complementary", "homogeneous",
		                                                    			"hierarchical", "alternative", "environmental", "inductive", "transitional", "Philosophical",
		                                                    			"latent", "conscious", "practical", "temperamental", "impersonal", "personal", "subjective",
		                                                    			"objective", "dynamic", "inclusive", "paradoxical", "pure", "central", "psychic", "associative",
		                                                    			"intuitive", "free-floating", "empirical", "superficial", "predominant", "actual", "mutual",
		                                                    			"arbitrary", "inevitable", "immediate", "affirming", "functional", "referential",
		                                                    			"numinous", "paralyptic", "radical", "paratheoretical", "consistent", "interpersonal",
		                                                    			"auxiliary", "empirical", "theoretical", "reciprocal", "unprejudiced", "transitional",
		                                                    			"incremental", "equivalent", "universal", "sub-logical", "hypothetical", "conjectural",
		                                                    			"conceptual ", "empirical", "spatio-temporal", "epistemological", "diffusible", "specific",
		                                                    			"non-referent", "overriding", "politico-strategical", "economico-social", "on-going",
		                                                    			"extrinsic", "intrinsic", "effective", "principal", "prime", "major", "empirical", "definitive",
		                                                    			"explicit", "determinant", "precise", "cardinal", "geometric", "naturalistic", "linear",
		                                                    			"distinctive", "phylogenetic", "ethical", "theoretical", "economic", "aesthetic",
		                                                    			"personal", "social", "discordant", "political", "religious", "artificial", "collective",
		                                                    			"permanent", "metaphysical", "organic", "mensurable", "expressive", "governing",
		                                                    			"subjective", "empathic", "imaginative", "ethical", "expressionistic", "resonant", "vibrant"
		                                                    	};

		static readonly string[] s_NounPhrases = new[]
		                                         	{
		                                         			"|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o",
		                                         			"|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o", "|o",
		                                         			"development", "program", "baseline", "reconstruction", "discordance",
		                                         			"monologism", "substructure", "legitimisation", "principle", "constraints",
		                                         			"management option", "strategy", "transposition", "auto-interruption",
		                                         			"derivation", "option", "flexibility", "proposal", "formulation", "item", "issue",
		                                         			"capability", "mobility", "programming", "concept", "time-phase", "dimension",
		                                         			"faculty", "capacity", "proficiency", "reciprocity", "fragmentation", "consolidation",
		                                         			"projection", "interface", "hardware", "contingency", "dialog", "dichotomy",
		                                         			"concept", "parameter", "algorithm", "milieu", "terms of reference", "item", "vibrancy",
		                                         			"reaction", "casuistry", "theme", "teleology", "symbolism", "resource allocation",
		                                         			"certification project", "functionality", "specification", "matrix", "rationalization",
		                                         			"consolidation", "remediation", "facilitation", "simulation", "evaluation", "competence",
		                                         			"familiarisation", "transformation", "apriorism", "conventionalism", "verification",
		                                         			"functionality", "component", "factor", "antitheseis", "desiderata", "metaphor",
		                                         			"metalanguage", "globalisation", "initiative", "projection", "partnership", "priority",
		                                         			"service", "support", "best-practice", "change", "delivery", "funding", "resources"
		                                         	};

		static readonly string[] s_Cliches = new[]
		                                     	{
		                                     			"|o of |o", "|o of |o", "|o of |o", "|o of |o", "|o of |o", "|o of |o", "|o of |o", "|o of |o",
		                                     			"development strategy", "decision support", "fourth-generation environment",
		                                     			"application systems", "feedback process", "function hierarchy analysis",
		                                     			"structured business analysis", "base information", "final consolidation",
		                                     			"design criteria", "iterative design process", "common interface",
		                                     			"ongoing support", "relational flexibility", "referential integrity",
		                                     			"strategic framework", "dynamic systems strategy", "functional decomposition",
		                                     			"operational situation", "individual action plan", "key behavioural skills",
		                                     			"set of constraints", "structure plan", "contingency planning", "resource planning",
		                                     			"participant feedback", "referential function", "passive result", "aims and constraints",
		                                     			"strategic opportunity", "development of systems resource", "major theme of the |c",
		                                     			"technical coherence", "cost-effective application", "high leverage area",
		                                     			"key leveraging technology", "known strategic opportunity", "internal resource capability",
		                                     			"interactive concern-control system", "key technology", "prime objective",
		                                     			"key area of opportunity", "present infrastructure", "enabling technology",
		                                     			"key objective", "areas of particular expertise", "overall business benefit",
		                                     			"competitive practice and technology", "flexible manufacturing system",
		                                     			"adequate resource level", "|e sophisticated hardware", "external agencies",
		                                     			"anticipated fourth-generation equipment", "maintenance of current standards",
		                                     			"adequate development of any necessary measures", "critical component in the",
		                                     			"active process of information gathering", "general milestones",
		                                     			"adequate timing control", "quantitative and discrete targets",
		                                     			"subsystem compatibility testing", "structural design, based on system engineering concepts",
		                                     			"key principles behind the |c", "constraints of manpower resourcing",
		                                     			"necessity for budgetary control", "discipline of resource planning",
		                                     			"diverse hardware environment", "product lead times", "access to corporate systems",
		                                     			"overall certification project", "commitment to industry standards",
		                                     			"general increase in office efficiency", "preliminary qualification limit",
		                                     			"calculus of consequence", "corollary", "reverse image", "logical data structure",
		                                     			"philosophy of commonality and standardization", "impact on overall performance",
		                                     			"multilingual cynicism", "functional synergy", "backbone of connectivity",
		                                     			"integrated set of requirements", "ongoing |3 philosophy", "strategic requirements",
		                                     			"integration of |c with strategic initiatives",
		                                     			"established analysis and design methodology", "corporate information exchange",
		                                     			"separate roles and significances of the |c", "formal strategic direction",
		                                     			"integrated set of facilities", "appreciation of vested responsibilities",
		                                     			"potential globalisation candidate", "tentative priority", "performance objectives",
		                                     			"global business practice", "functionality matrix", "priority sequence",
		                                     			"system elements", "life cycle phase", "operations scenario", " total system rationale",
		                                     			"conceptual baseline", "incremental delivery", "requirements hierarchy",
		                                     			"functional baseline", "system critical design", "capability constraint",
		                                     			"matrix of supporting elements", "lead group concept", "dominant factor",
		                                     			"modest correction", "element of volatility", "inevitability of amelioration",
		                                     			"attenuation of subsequent feedback", "chance of entropy within the system",
		                                     			"associated supporting element", "intrinsic homeostasis within the metasystem",
		                                     			"characterization of specific information", "organization structure",
		                                     			"constant flow of effective information", "key business objectives", "life cycle",
		                                     			"large portion of the co-ordination of communication",
		                                     			"corporate procedure", "proposed scenario"
		                                     	};

		static readonly string[] s_Prefixes = new[]
		                                      	{
		                                      			"the", "the", "the", "the", "the", "the", "the", "the", "the", "any", "any",
		                                      			"what might be described as the", "what amounts to the",
		                                      			"a large proportion of the", "what has been termed the",
		                                      			"a unique facet of the", "a significant aspect of the",
		                                      			"the all-inclusiveness of the", "any inherent dangers of the",
		                                      			"the obvious necessity for the", "the basis of any", "the basis of the",
		                                      			"any formalization of the", "the quest for the",
		                                      			"any significant enhancements in the", "the underlying surrealism of the ",
		                                      			"the feasibility of the", "the requirements of", "an implementation strategy for",
		                                      			"any fundamental dichotomies of the", "a concept of what we have come to call the", "the infrastructure of the",
		                                      			"a proven solution to the", "a percentage of the", "a proportion of the",
		                                      			"an issue of the", "any consideration of the", "a factor within the",
		                                      			"the adequate functionality of the", "the principle of the",
		                                      			"the constraints of the", "a realization the importance of the",
		                                      			"the criterion of", "a unique facet of", "the consolidation of the",
		                                      			"a preponderance of the"
		                                      	};

		static readonly string[] s_ArtyNouns = new[]
		                                       	{
		                                       			"discordance", "legitimisation", "principle", "transposition", "dimension",
		                                       			"reciprocity", "fragmentation", "projection", "dichotomy", "concept", "theme", "teleology",
		                                       			"symbolism", "transformation", "antithesis", "desiderata", "metaphor", "metalanguage",
		                                       			"reciprocity", "consciousness", "feeling", "fact", "individuality", "comparison", "awareness",
		                                       			"expression", "appreciation", "correspondence", "interpretation", "interpolation", "interpenetration",
		                                       			"statement", "emphasis", "feeling", "empathy", "sensibility", "insight", "attitude", "consciousness",
		                                       			"absorbtion", "self-forgetfulness", "parallelism", "classification", "evidence", "aspect", "distinction",
		                                       			"idealism", "naturalism", "disposition", "apprehension", "morality", "object", "idealism", "quality",
		                                       			"romanticism", "realism", "idealism", "quality", "transposition", "determinism", "attitude", "terminology",
		                                       			"individuality", "category", "integration", "concept", "phenomenon", "element", "analogy", "perception",
		                                       			"principle", "aesthetic", "spirituality", "aspiration", "quality", "disposition", "subjectivism", "objectivism",
		                                       			"contemplation", "vivacity", "feeling", "empathy", "value", "sensation", "causation", "affectability", "impulse",
		                                       			"attitude", "sensibility", "material", "aspect", "problem", "implication", "hierarchy", "process", "provenance",
		                                       			"discord", "milieu"
		                                       	};

		static readonly string[] s_Surnames = new[]
		                                      	{
		                                      			"Bennet", "Blotchet-Halls", "Carson", "Clarke", "DeFrance", "del Castillo", "Dull", "Green", "Greene",
		                                      			"Gringlesby", "Hunter", "Karsen", "Locksley", "MacFeather", "McBadden", "O'Leary", "Panteley", "Poel", "Powys-Lybbe", "Smith",
		                                      			"Straight", "Stringer", "White", "Yokomoto"
		                                      	};

		static readonly string[] s_Forenames = new[]
		                                       	{
		                                       			"Abraham", "Reginald", "Cheryl", "Michel", "Innes", "Ann", "Marjorie", "Matthew", "Mark", "Luke", "John",
		                                       			"Burt", "Lionel", "Humphrey", "Andrew", "Jenny", "Sheryl", "Livia", "Charlene", "Winston", "Heather", "Michael", "Sylvia", "Albert",
		                                       			"Anne", "Meander", "Dean", "Dirk", "Desmond", "Akiko"
		                                       	};

		static readonly string[] s_Buzzphrases = new[]
		                                         	{
		                                         			"|1 |2 |3",
		                                         			"|1 |2 |3",
		                                         			"|2 |3",
		                                         			"|1 |2 |3",
		                                         			"|1 |2 |3",
		                                         			"|4",
		                                         			"|4"
		                                         	};

		static readonly string[] s_CardinalSequence = new[]
		                                              	{
		                                              			"one", "two", "three", "four", "five", "six",
		                                              			"seven", "eight", "nine", "ten", "eleven", "twelve"
		                                              	};

		static readonly string[] s_OrdinalSequences = new[]
		                                              	{
		                                              			"first", "second", "third", "fourth", "fifth"
		                                              	};

		static readonly string[] s_MaybeHeading = new[]
		                                          	{
		                                          			"", "", "\n<h2>The |uc.</h2>\n<p>", ""
		                                          	};

		static readonly string[] s_MaybeParagraph = new[]
		                                            	{
		                                            			"", "", "|n", ""
		                                            	};

		readonly Random m_Random;

		string m_Title;
		int m_CardinalSequence;
		int m_OrdinalSequence;


		public WaffleEngine( Random random )
		{
			m_Random = random;
		}


		void EvaluateRandomPhrase( string[] phrases, StringBuilder output )
		{
			EvaluatePhrase( phrases[m_Random.Next( 0, phrases.Length )], output );
		}


		void EvaluatePhrase( string phrase, StringBuilder result )
		{
			for ( int i = 0; i < phrase.Length; i++ )
			{
				if ( phrase[i] == '|' && i + 1 < phrase.Length )
				{
					i++;

					StringBuilder escape = result;
					bool titleCase = false;

					if ( phrase[i] == 'u' && i + 1 < phrase.Length )
					{
						escape = new StringBuilder();
						titleCase = true;
						i++;
					}

					switch ( phrase[i] )
					{
						// Cardinal Sequence: "|a |a |a" => "one two three"
						case 'a':
							EvaluateCardinalSequence( escape );
							break;

						// Ordinal Sequence: "|b |b |b" => "first second third"
						case 'b':
							EvaluateOrdinalSequence( escape );
							break;

						case 'c':
							EvaluateRandomPhrase( s_Buzzphrases, escape );
							break;
						case 'd':
							EvaluateRandomPhrase( s_Verbs, escape );
							break;
						case 'e':
							EvaluateRandomPhrase( s_Adverbs, escape );
							break;
						case 'f':
							EvaluateRandomPhrase( s_Forenames, escape );
							break;
						case 's':
							EvaluateRandomPhrase( s_Surnames, escape );
							break;

						// Noun
						case 'o':
							EvaluateRandomPhrase( s_ArtyNouns, escape );
							break;

						// Date
						case 'y':
							RandomDate( escape );
							break;

						// Prefix Phrase: (i.e. "the", "an issue of the...", etc)
						case 'h':
							EvaluateRandomPhrase( s_Prefixes, escape );
							break;
						case 'A':
							EvaluateRandomPhrase( s_PreamblePhrases, escape );
							break;
						case 'B':
							EvaluateRandomPhrase( s_SubjectPhrases, escape );
							break;
						case 'C':
							EvaluateRandomPhrase( s_VerbPhrases, escape );
							break;
						case 'D':
							EvaluateRandomPhrase( s_ObjectPhrases, escape );
							break;

						// First Adjective Phrase
						case '1':
							EvaluateRandomPhrase( s_FirstAdjectivePhrases, escape );
							break;

						// Second Adjective Phrase
						case '2':
							EvaluateRandomPhrase( s_SecondAdjectivePhrases, escape );
							break;

						// Noun Phrase
						case '3':
							EvaluateRandomPhrase( s_NounPhrases, escape );
							break;

						// Cliche Phrase
						case '4':
							EvaluateRandomPhrase( s_Cliches, escape );
							break;

						// Title
						case 't':
							escape.Append( m_Title );
							break;

						// Empty paragraph (HTML)
						case 'n':
							escape.Append( "</p>\n<p>" );
							break;
					}

					if ( titleCase )
						result.Append( TitleCaseWords( escape.ToString() ) );
				}
				else
					result.Append( phrase[i] );
			}
		}


		void EvaluateCardinalSequence( StringBuilder output )
		{
			if ( m_CardinalSequence >= s_CardinalSequence.Length )
				m_CardinalSequence = 0;

			output.Append( s_CardinalSequence[m_CardinalSequence++] );
		}


		void EvaluateOrdinalSequence( StringBuilder output )
		{
			if ( m_OrdinalSequence >= s_OrdinalSequences.Length )
				m_OrdinalSequence = 0;

			output.Append( s_OrdinalSequences[m_OrdinalSequence++] );
		}


		void RandomDate( StringBuilder output )
		{
			output.AppendFormat( "{0:04u}", DateTime.Now.Year - m_Random.Next( 0, 31 ) );
		}


		public static string TitleCaseWords( string input )
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase( input );
		}


		public void HtmlWaffle( int paragraphs, Boolean includeHeading, StringBuilder result )
		{
			m_Title = string.Empty;
			m_CardinalSequence = 0;
			m_OrdinalSequence = 0;

			if ( includeHeading )
			{
				var title = new StringBuilder();
				EvaluatePhrase( "the |o of |2 |o", title );

				m_Title = TitleCaseWords( title.ToString() );

				result.AppendLine( "<html>" );
				result.AppendLine( "<head>" );
				result.AppendFormat( "<title>{0}</title>", m_Title );
				result.AppendLine();
				result.AppendLine( "</head>" );
				result.AppendLine( "<body>" );
				result.AppendFormat( @"<h1>{0}</h1>", m_Title );
				result.AppendLine();
				EvaluatePhrase( "<blockquote>\"|A |B |C |t\"<br>", result );
				EvaluatePhrase( "<cite>|f |s in The Journal of the |uc (|uy)</cite></blockquote>", result );
				result.AppendLine();
				EvaluatePhrase( "<h2>|c.</h2>", result );
				result.AppendLine();
			}
			result.Append( "<p>" );

			for ( int i = 0; i < paragraphs; i++ )
			{
				if ( i != 0 )
					EvaluateRandomPhrase( s_MaybeHeading, result );

				EvaluatePhrase( "|A |B |C |D.  ", result );
				EvaluateRandomPhrase( s_MaybeParagraph, result );
			}

			result.AppendLine( "</p>" );
			result.AppendLine( "</body>" );
			result.AppendLine( "</html>" );
		}


		public void TextWaffle( int paragraphs, Boolean includeHeading, StringBuilder result )
		{
			m_Title = string.Empty;
			m_CardinalSequence = 0;
			m_OrdinalSequence = 0;

			if ( includeHeading )
			{
				var title = new StringBuilder();
				EvaluatePhrase( "the |o of |2 |o", title );

				m_Title = TitleCaseWords( title.ToString() );

				result.AppendLine( m_Title );
				result.AppendLine();
				EvaluatePhrase( "\"|A |B |C |t\"\n", result );
				EvaluatePhrase( "(|f |s in The Journal of the |uc (|uy))", result );
				result.AppendLine();
				EvaluatePhrase( "|c.", result );
				result.AppendLine();
			}

			for ( int i = 0; i < paragraphs; i++ )
			{
				if ( i != 0 )
					EvaluateRandomPhrase( s_MaybeHeading, result );

				EvaluatePhrase( "|A |B |C |D.  ", result );
				EvaluateRandomPhrase( s_MaybeParagraph, result );
			}
		}
	}
}