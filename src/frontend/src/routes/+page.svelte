<script lang="ts">
	import { goto } from "$app/navigation";

	let boards = [];

	for (let i = 0; i < 10; i++) {
		boards.push({
            id: i,
			name: i.toString(),
			description:
				'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'
		});
	}
</script>

<div class="mb-4 pt-0 p-4">
	{#each boards as board}
		<button
			class="bg-gray-200 dark:bg-gray-800 p-1 m-1
			rounded text-center hover:cursor-pointer
			hover:bg-gray-300 hover:dark:bg-gray-700"
            on:click={() => {
                goto("/boards/" + board.id);
            }}
		>
			<h2 class="text-xl"><strong>{board.name}</strong></h2>
			<sub class="text-md">{board.description}</sub>
        </button>
	{/each}
</div>
