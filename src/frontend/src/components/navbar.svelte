<script lang="ts">
	import { token } from "../hooks/auth";
    import { jwtDecode, type JwtPayload } from "jwt-decode";
    import { Navbar, NavBrand, NavLi, NavUl, NavHamburger, Avatar, Dropdown, DropdownItem, DropdownHeader, DropdownDivider, DarkMode } from 'flowbite-svelte'
    import NoIcon from "$lib/assets/no-icon.png";

    export let title;

    let decodedToken: JwtPayload;
    let username: string = "Guest";
    let email: string = "john.doe@email.com"

    $: token;

    if($token)
    {
        decodedToken = jwtDecode($token);
        username = decodedToken.username!;
        email = decodedToken.email!;
    }
</script>

<Navbar color="gray" class="bg-gray-200 dark:bg-gray-900">
    <NavBrand href="/" class="p-2">
        <img src={NoIcon} class="me-3 h-6 sm:h-9" alt="Flowbite Logo" />
        <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">{title}</span>
    </NavBrand>
    {#if $token}
    <div class="flex items-center md:order-2 hover:cursor-pointer">
        <NavHamburger class1="w-full md:flex md:w-auto md:order-1" />
        <div class="hover:dark:bg-gray-700 hover:bg-gray-100 rounded p-2">
            <Avatar id="avatar-menu" src={NoIcon} />
        </div>

        <DarkMode class="ml-1 p-5" />
    </div>
    <Dropdown class="hover:cursor-pointer" placement="bottom" triggeredBy="#avatar-menu">
        <DropdownHeader>
            <span class="block text-sm">{username}</span>
            <sub class="block truncate text-xs">{email}</sub>
        </DropdownHeader>
        <DropdownItem href="/manage">Dashboard</DropdownItem>
        <DropdownItem>Settings</DropdownItem>
        <DropdownItem>Earnings</DropdownItem>
        <DropdownDivider />
        <DropdownItem href="/logout">Sign out</DropdownItem>
    </Dropdown>
    {:else}
    <div class="flex items-center md:order-2 hover:cursor-pointer">
        <NavHamburger class="w-full md:flex md:w-auto md:order-1" />
        <div class="hover:bg-gray-700 rounded p-2">
            <Avatar id="avatar-menu" src={NoIcon} />
        </div>
        <DarkMode class="ml-2" />
    </div>
    <Dropdown class="hover:cursor-pointer" placement="bottom" triggeredBy="#avatar-menu">
        <DropdownHeader>
            <span class="block text-sm">Guest</span>
        </DropdownHeader>
        <DropdownItem href="/register">Register</DropdownItem>
        <DropdownItem href="/login">Sign in</DropdownItem>
    </Dropdown>
    {/if}
    <NavUl>
        <NavLi href="/" active={true}>Home</NavLi>
        <NavLi href="/about">About</NavLi>
        <NavLi href="/docs/components/navbar">Navbar</NavLi>
        <NavLi href="/pricing">Pricing</NavLi>
        <NavLi href="/contact">Contact</NavLi>
    </NavUl>
</Navbar>
