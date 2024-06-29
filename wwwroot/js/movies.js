document.addEventListener("DOMContentLoaded", function () {
    fetchMovies();
});

async function fetchMovies() {
    showLoadingSpinner();
    const response = await fetch("/api/movies");
    const movies = await response.json();
    hideLoadingSpinner();
    renderMovies(movies);
}

function renderMovies(movies) {
    const moviesContainer = document.getElementById("movies-container");
    moviesContainer.innerHTML = "";

    movies.forEach(movie => {
        const movieCard = document.createElement("div");
        movieCard.classList.add("col-md-3", "mb-3");
        movieCard.innerHTML = `
            <div class="card h-100 text-white bg-dark">
                <img class="card-img-top" src="${movie.posterPath}" alt="${movie.title}" />
                <div class="card-body">
                    <h5 class="card-title">${movie.title}</h5>
                    <p class="card-text">
                        <strong>Release Date:</strong> ${new Date(movie.releaseDate).toLocaleDateString()}<br />
                        <strong>Genre:</strong> ${movie.genre}<br />
                        <strong>Rating:</strong> ${movie.rating}
                    </p>
                </div>
                <div class="card-footer">
                    <a class="btn btn-sm btn-primary" href="/Movies/Edit?id=${movie.id}">Edit</a>
                    <a class="btn btn-sm btn-secondary" href="/Movies/Details?id=${movie.id}">Details</a>
                    <a class="btn btn-sm btn-danger" href="/Movies/Delete?id=${movie.id}">Delete</a>
                </div>
            </div>`;
        moviesContainer.appendChild(movieCard);
    });
}

function applyFilters() {
    const genreFilter = document.getElementById("genre-filter").value;
    const sortOption = document.getElementById("sort-options").value;

    fetchFilteredAndSortedMovies(genreFilter, sortOption);
}

async function fetchFilteredAndSortedMovies(genre, sortOption) {
    showLoadingSpinner();
    const response = await fetch(`/api/movies?genre=${genre}&sortOption=${sortOption}`);
    const movies = await response.json();
    hideLoadingSpinner();
    renderMovies(movies);
}

function showLoadingSpinner() {
    document.getElementById("loading-spinner").style.display = "flex";
    document.getElementById("movies-container").style.display = "none";
}

function hideLoadingSpinner() {
    document.getElementById("loading-spinner").style.display = "none";
    document.getElementById("movies-container").style.display = "flex";
}
