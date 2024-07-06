<script lang="ts">
    let boards = [{name:"Jeffy", description: "jeffy"}, {name:"y", description: "jeffys a freak"}, {name:"z", description: "jeffys weird"}];
</script>

<div class="my-4 pt-0 p-4">
    {#each boards as board}
        <div class="bg-gray-200 dark:bg-gray-800 p-2 m-1 rounded text-center hover:cursor-pointer hover:bg-gray-300 hover:dark:bg-gray-700">
            <h2 class="text-xl"><strong>{board.name}</strong></h2>
            <sub class="text-md">{board.description}</sub>
        </div>
    {/each}
</div>