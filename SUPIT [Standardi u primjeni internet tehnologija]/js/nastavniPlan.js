const search = document.getElementById('search');
const matchList = document.getElementById('match-list');

// Search kolegij.json
const searchKolegij = async searchNaziv =>
{
    const res = await fetch('../data/kolegij.json');
    const kolegij = await res.json();

    // Get match za upisani tekst
    let matches = kolegij.filter
    (
        koleg => {
            const regex = new RegExp(`^${searchNaziv}`, 'gi');
            return koleg.label.match(regex);
    });

    if(searchNaziv.length === 0)
    {
        matches = [];
        matchList.innerHTML= '';
    }

    outputHtml(matches);
};

const outputHtml = matches =>
{
    if(matches.length > 0)
    {
        const html = matches.map(match => `
            <div class = "card card-body mb-1">
                <h4>${match.label} <span class = "text-primary">
                </h4>
            </div>
        `).join('');

    matchList.innerHTML = html;
    }
}

search.addEventListener('input', () => searchKolegij(search.value));



