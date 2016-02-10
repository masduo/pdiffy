webpackJsonp([2,3],[
/* 0 */
/***/ function(module, exports) {

	console.log('hello from text differences.js ...');

	var filteredSites = '';
	var filteredPages = '';

	init();

	function init() {
		bindToggleButtons();
	}

	function bindToggleButtons() {
		var toggles = document.getElementsByName('toggleButton');
		console.log('elements count: ' + toggles.length);

		for (var i = 0; i < toggles.length; i++) {
			var toggle = toggles[i];
			console.log(toggle.id.replace('cmn-toggle-', ''));
			toggle.onclick = function() { filter(this.getAttribute('toggles'), this.id.replace('cmn-toggle-', '')); };
		}
	}

	function filter(filterClass, siteOrPage) {
		//window.history.pushState({ site: site }, "changed", "/#www");
		console.log(filterClass, siteOrPage);

		if (filterClass == 'site') {
			if (filteredSites.indexOf(siteOrPage) < 0)
				filteredSites += " " + siteOrPage;
			else
				filteredSites = filteredSites.replace(siteOrPage, '');
		}

		if (filterClass == 'page') {
			if (filteredPages.indexOf(siteOrPage) < 0)
				filteredPages += " " + siteOrPage;
			else
				filteredPages = filteredPages.replace(siteOrPage, '');
		}
		filteredSites = filteredSites.trim();
		filteredPages = filteredPages.trim();

		var siteSpans = document.getElementsByClassName('site');
		var pageSpans = document.getElementsByClassName('page');

		for (var i = 0; i < siteSpans.length; i++) {
			if (filteredSites == '' && filteredPages == '') {
				//show all
				siteSpans[i].parentElement.style.display = 'block';
				pageSpans[i].parentElement.style.display = 'block';
				continue;
			}

			var inSites = filteredSites.indexOf(siteSpans[i].innerText) >= 0;
			var inPages = filteredPages.indexOf(pageSpans[i].innerText) >= 0;

			if (inSites && (filteredPages == '' || inPages)) {
				siteSpans[i].parentElement.style.display = 'block';
			}
			else {
				siteSpans[i].parentElement.style.display = 'none';

				if (inPages && (filteredSites == '' || inSites)) {
					siteSpans[i].parentElement.style.display = 'block';
				} else {
					siteSpans[i].parentElement.style.display = 'none';
				}
			}
		}
		return false;
	}


/***/ }
]);
