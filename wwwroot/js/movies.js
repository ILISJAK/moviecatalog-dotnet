document.addEventListener("DOMContentLoaded", function () {
    fetchMovies();
});

async function fetchMovies() {
    const response = await fetch("/api/movies");
    const movies = await response.json();

    const moviesTable = document.getElementById("movies-table-body");
    moviesTable.innerHTML = "";

    movies.forEach(movie => {
        const row = document.createElement("tr");

        const titleCell = document.createElement("td");
        titleCell.textContent = movie.title;
        row.appendChild(titleCell);

        const releaseDateCell = document.createElement("td");
        releaseDateCell.textContent = new Date(movie.releaseDate).toLocaleDateString();
        row.appendChild(releaseDateCell);

        const genreCell = document.createElement("td");
        genreCell.textContent = movie.genre;
        row.appendChild(genreCell);

        const ratingCell = document.createElement("td");
        ratingCell.textContent = movie.rating;
        row.appendChild(ratingCell);

        const actionsCell = document.createElement("td");
        actionsCell.innerHTML = `<a href="/Movies/Edit?id=${movie.id}">Edit</a> | <a href="/Movies/Details?id=${movie.id}">Details</a> | <a href="/Movies/Delete?id=${movie.id}">Delete</a>`;
        row.appendChild(actionsCell);

        moviesTable.appendChild(row);
    });
}
