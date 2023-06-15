import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import SinglePlayer from '../SinglePlayer.vue'

describe('SinglePlayer', () => {
  it('renders properly', () => {
    const wrapper = mount(SinglePlayer, { props: { msg: 'Game!' } })
    expect(wrapper.text()).toContain('Game!')
  })
})