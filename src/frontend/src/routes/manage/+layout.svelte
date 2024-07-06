<script lang="ts">
	import "../../app.css";
    import {
		Sidebar,
		SidebarGroup,
		SidebarItem,
		SidebarWrapper,
		SidebarDropdownWrapper
	} from 'flowbite-svelte';
</script>


<div class="flex flex-row">
    <Sidebar asideClass="h-full w-64 mr-6">
        <SidebarWrapper divClass="overflow-y-auto py-4 px-3 rounded dark:bg-gray-800">
            <SidebarGroup>
                <SidebarItem label="Home" href="/" />
                <!-- {#each data.pages as { meta, path }}
                    <SidebarItem
                        label={meta.title}
                        href={`/pages/${path}`}
                        {spanClass}
                        activeClass="flex items-center p-2 text-base font-normal text-gray-900 bg-primary-200 dark:bg-primary-700 rounded-lg dark:text-white hover:bg-primary-100 dark:hover:bg-primary-700"
                        on:click={toggleSide}
                        active={activeUrl === `/pages/${path}`}
                    />
                {/each} -->
                <SidebarDropdownWrapper label="Articles">
                    <!-- {#each data.articles as { meta, path }}
                        <SidebarItem
                            label={meta.title}
                            href={`/blog/${path}`}
                            {spanClass}
                            activeClass="flex items-center p-2 text-base font-normal text-gray-900 bg-primary-200 dark:bg-primary-700 rounded-lg dark:text-white hover:bg-primary-100 dark:hover:bg-primary-700"
                            on:click={toggleSide}
                            active={activeUrl === `/blog/${path}`}
                        />
                    {/each} -->
                </SidebarDropdownWrapper>
            </SidebarGroup>
        </SidebarWrapper>
    </Sidebar>
	<slot />
</div>
