function Filter() { this.init(); }

Filter.prototype.init = function () {
	this.bindToggleButtons();
}

Filter.prototype.bindToggleButtons = function () {
	var that = this;
	var toggles = document.getElementsByClassName('input-toggle');

	for (var i = 0; i < toggles.length; i++)
		toggles[i].onclick = function () { that.filter(); };
}

/// none selected: show none 
/// site only: show all pages of each selected site
/// page only: show selected pages of all sites
/// site and page: show selected pages of selected sites
Filter.prototype.filter = function () {
	var checkedSites = this.getInputTexts('site');
	var checkedPages = this.getInputTexts('page');

	var siteSpans = document.getElementsByClassName('site');

	for (var i = 0; i < siteSpans.length; i++) {
		var siteSpan = siteSpans[i];
		var pageSpan = siteSpan.nextElementSibling;
		var parentElement = siteSpan.parentElement;
		parentElement.style.display = 'none';

		if (checkedSites.length == 0 && checkedPages.length == 0) {
			parentElement.style.display = 'none';
		} else {
			var inSites = checkedSites.indexOf(siteSpan.innerText) >= 0;
			var inPages = checkedPages.indexOf(pageSpan.innerText) >= 0;

			if (inSites && inPages ||
				inSites && checkedPages.length == 0 ||
				inPages && checkedSites.length == 0) {
				parentElement.style.display = 'block';
			}
		}
	}
}

Filter.prototype.getInputTexts = function (toggle) {
	var checkedInputs = document.querySelectorAll('input.input-toggle[toggles=' + toggle + ']:checked');
	var filters = '';
	for (var i = 0; i < checkedInputs.length; i++)
		filters += ' ' + checkedInputs[i].nextElementSibling.innerText;

	//console.log(toggle + " filters: " + filters);
	return filters;
}

var theFilter = new Filter();